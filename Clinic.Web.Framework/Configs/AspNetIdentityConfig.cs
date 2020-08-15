using System;
using Clinic.Core.Configuration;
using Clinic.Core.Infrastructure.DependencyManagement;
using Clinic.Service.Services.Users;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using StructureMap.Web;

namespace Clinic.Web.Framework.Configs
{

    public class AspNetIdentityConfig
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        public static void RegisterAspNetIdentity(IAppBuilder appBuilder)
        {
            const int twoWeeks = 14;

            ContainerManager.Container.Configure(config => config.For<IDataProtectionProvider>().HybridHttpOrThreadLocalScoped().Use(() => appBuilder.GetDataProtectionProvider()));

            appBuilder.CreatePerOwinContext(() => ContainerManager.Container.GetInstance<IUserService>());

            appBuilder.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);

            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                ExpireTimeSpan = TimeSpan.FromDays(twoWeeks),
                SlidingExpiration = true,
                CookieName = "_clinic",
                // CookieDomain = ContainerManager.Container.GetInstance<IConfigurationManager>().CookieIsLocalhost ? "localhost" : ".novinak.com",
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = ContainerManager.Container.GetInstance<IUserService>().OnValidateIdentity()
                }
            });

            appBuilder.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ContainerManager.Container.GetInstance<IConfigurationManager>().GoogleClientId,
                ClientSecret = ContainerManager.Container.GetInstance<IConfigurationManager>().GoogleCientSecret,
                Provider = new GoogleOAuth2AuthenticationProvider(),
                CallbackPath = new PathString("/signin-google")
            };

            appBuilder.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);

            //StructureMapObjectFactory.Container.GetInstance<IRoleService>().SeedDatabase();

            //StructureMapObjectFactory.Container.GetInstance<IUserService>().SeedDatabase();


        }

        private static object GetUserServiceInstance()
        {
            return ContainerManager.Container.GetInstance<IUserService>();
        }

        #endregion Public Methods
    }
}