using Clinic.Web.Framework.StructureMap;

namespace Clinic.Web.Framework.Configs
{

    public class StructureMapConfig
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        public static void RegisterStructureMap()
        {
            ConfigureServiceLayer();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///
        /// </summary>
        private static void ConfigureServiceLayer()
        {
            StructureMapObjectFactory.DefaultContainer();

            //SubscriptionService.Container = ContainerManager.Container;
            //ProductExtensions.ProductService = ContainerManager.Container.GetInstance<IProductService>();
            //ProductExtensions.WebContextManager = ContainerManager.Container.GetInstance<IWebContextManager>();
            //CompanyExtensions.CompanyImageService = ContainerManager.Container.GetInstance<ICompanyImageService>();
            //CompanyExtensions.CompanyAttachmentService = ContainerManager.Container.GetInstance<ICompanyAttachmentService>();
            //CompanyExtensions.ContextManager = ContainerManager.Container.GetInstance<IWebContextManager>();
        }

        #endregion Private Methods
    }
}