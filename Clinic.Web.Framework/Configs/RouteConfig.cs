using System.Web.Mvc;
using System.Web.Routing;
using Clinic.Core.Configuration;
using Clinic.Core.Infrastructure.DependencyManagement;

namespace Clinic.Web.Framework.Configs
{
    /// <summary>
    /// </summary>
    public class RouteConfig
    {
        #region Public Properties

        public static IConfigurationManager ConfigurationManager => ContainerManager.Container.GetInstance<IConfigurationManager>();

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            #region IgnoreRoutes

            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.ico");
            routes.IgnoreRoute("{resource}.png");
            routes.IgnoreRoute("{resource}.jpg");
            routes.IgnoreRoute("{resource}.gif");
            routes.IgnoreRoute("{resource}.txt");
            routes.IgnoreRoute("elmah.axd");

            #endregion IgnoreRoutes

            routes.LowercaseUrls = true;

            routes.MapMvcAttributeRoutes();

            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                controller = "Home",
                action = "Dashboard",
                id = UrlParameter.Optional
            }, new[]
            {
                ConfigurationManager.WebControllers
            });
        }

        #endregion Public Methods
    }
}