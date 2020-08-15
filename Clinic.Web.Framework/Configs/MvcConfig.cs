using System;
using System.Linq;
using System.Web.Mvc;
using Clinic.FrameWork.ModelBinders;
using Clinic.Web.Framework.StructureMap;

namespace Clinic.Web.Framework.Configs
{
    public class MvcConfig
    {
        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        public static void RegisterMvc()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            var filterProider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProider);
            FilterProviders.Providers.Add(new StructureMapFilterProvider());

            //var defaultJsonFactory = ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault();
            //var index = ValueProviderFactories.Factories.IndexOf(defaultJsonFactory);
            //ValueProviderFactories.Factories.Remove(defaultJsonFactory);
            //ValueProviderFactories.Factories.Insert(index, new JsonNetValueProviderFactory());

            System.Web.Mvc.ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        #endregion Public Methods
    }
}