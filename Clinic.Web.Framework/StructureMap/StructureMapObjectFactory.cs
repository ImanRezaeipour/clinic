using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Clinic.Core.Configuration;
using Clinic.Core.Infrastructure.DependencyManagement;
using Clinic.Data.Conventions;
using Clinic.Data.DbContexts;
using Clinic.FrameWork.Providers;
using Clinic.FrameWork.StructureMap.Registeries;
using Clinic.Service.Services.HttpContext;
using Clinic.Web.Framework.StructureMap.Registeries;
using StructureMap;
using StructureMap.Web;

namespace Clinic.Web.Framework.StructureMap
{
    /// <summary>
    /// </summary>
    public static class StructureMapObjectFactory
    {
      

        #region DefaultContainer

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static void DefaultContainer()
        {
            ContainerManager.Container.Configure(ioc =>
            {
                ioc.For<IIdentity>().Use(() => HttpContext.Current != null && HttpContext.Current.User != null ? HttpContext.Current.User.Identity : null);
                ioc.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<BaseDbContext>();
                ioc.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
                ioc.For<HttpServerUtilityBase>().Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));
                ioc.For<HttpRequestBase>().Use(ctx => ctx.GetInstance<HttpContextBase>().Request);
                ioc.For<ISessionProvider>().Use<SessionProvider>();
                ioc.For<IRemotingFormatter>().Use(ctx => new BinaryFormatter());
                ioc.For<ITempDataProvider>().Use<CookieTempDataProvider>();

                ioc.For<IConfigurationManager>().Use<ConfigurationManager>();
                ioc.For<IHttpContextManager>().Use<HttpContextManager>();
                ioc.For<IPluralizeConvention>().Use<PluralizeConvention>().Singleton();

                ioc.AddRegistry<AspNetIdentityRegistery>();
                ioc.AddRegistry<AutoMapperRegistery>();
                ioc.AddRegistry<ServiceLayerRegistery>();
                ioc.AddRegistry<TaskRegistry>();

                ioc.Scan(scanner =>
                {
                    //scanner.TheCallingAssembly();
                    //scan.AssemblyContainingType<SomeType>(); // for other asms, if any.
                    scanner.WithDefaultConventions();
                    //scanner.AddAllTypesOf<Profile>().NameBy(item => item.FullName);
                });

                ioc.Policies.SetAllProperties(y => y.OfType<HttpContextBase>());
            });

            ConfigureAutoMapper(ContainerManager.Container);

        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="container"></param>
        private static void ConfigureAutoMapper(IContainer container)
        {
            // Exception - unmapped member
            //container.GetInstance<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}