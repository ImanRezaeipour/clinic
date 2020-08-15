using System.Collections.Generic;
using System.Web.Optimization;

namespace Clinic.Web.Framework.Configs
{
    /// <summary>
    /// </summary>
    public class BundleConfig
    {
        #region Public Methods

        /// <summary>
        ///    ثبت کلیه باندل های سایت
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Styles

            bundles.Add(new StyleBundle("~/bundles/styles/css")

                //  Fonts
                .Include("~/Bundles/Vendors/Fonts/iransans.css")

                //  Toastr
                .Include("~/Bundles/Vendors/Toastr/toastr.min.css")
                .Include("~/Bundles/Vendors/jsPersianDatePicker/Content/PersianDatePicker.min.css")

                //  Altair
                .Include("~/Bundles/Vendors/altair/bower_components/weather-icons/css/weather-icons.min.css")
                .Include("~/Bundles/Vendors/altair/bower_components/metrics-graphics/dist/metricsgraphics.css")
                .Include("~/Bundles/Vendors/altair/bower_components/chartist/dist/chartist.min.css")
                .Include("~/Bundles/Vendors/altair/assets/css/uikit.rtl.css")
                .Include("~/Bundles/Vendors/altair/assets/icons/flags/flags.min.css")
                .Include("~/Bundles/Vendors/altair/assets/css/style_switcher.min.css")
                .Include("~/Bundles/Vendors/altair/assets/css/main.min.css")
                .Include("~/Bundles/Vendors/altair/assets/css/themes/themes_combined.min.css")

                //  KendoUI
                .Include("~/Bundles/Vendors/KendoUI/css/kendo.common.min.css")
                .Include("~/Bundles/Vendors/KendoUI/css/kendo.rtl.min.css")
                .Include("~/Bundles/Vendors/KendoUI/css/kendo.material.min.css")

                //  Custom
                .Include("~/Bundles/Styles/site.css"));

            #endregion Styles

            #region Bundles

            bundles.Add(new ScriptBundle("~/bundles/scripts/js")

                //  jQuery
                .Include("~/Bundles/Vendors/JQuery/jquery-3.1.1.min.js")

                //  jQueryValidate
                .Include("~/Bundles/Vendors/JQueryValidate/jquery.validate.min.js")
                .Include("~/Bundles/Vendors/JQueryValidate/jquery.validate.unobtrusive.min.js")

                //  Toastr
                .Include("~/Bundles/Vendors/Toastr/toastr.min.js")

                //  JQueryWebcam
                .Include("~/Bundles/Vendors/JQueryWebcam/jquery.webcam.js")

                //  JQueryWebcam
                .Include("~/Bundles/Vendors/TrackingJS/tracking-min.js")
                .Include("~/Bundles/Vendors/TrackingJS/data/face-min.js")

                // 
                .Include("~/Bundles/Vendors/jsPersianDatePicker/Scripts/PersianDatePicker.min.js")
                .Include("~/Bundles/Vendors/jsPersianDatePicker/Scripts/index.js")

                //  Parsley
                //.Include("~/Bundles/Customs/parsley.configs.js")
                //.Include("~/Bundles/Vendors/Parsley/parsley.min.js")
                //.Include("~/Bundles/Vendors/Parsley/i18n/fa.js")

                //  Altair
                .Include("~/Bundles/Vendors/altair/assets/js/common.min.js")
                .Include("~/Bundles/Vendors/altair/assets/js/uikit_custom.min.js")
                .Include("~/Bundles/Vendors/altair/assets/js/altair_admin_common.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/d3/d3.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/metrics-graphics/dist/metricsgraphics.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/chartist/dist/chartist.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/maplace-js/dist/maplace.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/peity/jquery.peity.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/jquery.easy-pie-chart/dist/jquery.easypiechart.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/countUp.js/dist/countUp.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/handlebars/handlebars.min.js")
                //.Include("~/Bundles/Vendors/altair/bower_components/parsleyjs/dist/parsley.min.js")
                .Include("~/Bundles/Vendors/altair/assets/js/custom/handlebars_helpers.min.js")
                .Include("~/Bundles/Vendors/altair/bower_components/clndr/clndr.min.js")
                .Include("~/Bundles/Vendors/altair/assets/js/pages/dashboard.min.js")
                .Include("~/Bundles/Scripts/Frameworks/parsley.configs.js")
                .Include("~/Bundles/Vendors/Parsley/parsley.min.js")
                .Include("~/Bundles/Vendors/Parsley/i18n/fa.js")

                //  KendoUI
                .Include("~/Bundles/Vendors/KendoUI/js/kendo.web.min.js")
                .Include("~/Bundles/Vendors/KendoUI/js/kendo.aspnetmvc.min.js")
                .Include("~/Bundles/Vendors/KendoUI/js/cultures/kendo.culture.fa-IR.min.js")
                .Include("~/Bundles/Vendors/KendoUI/js/cultures/kendo.culture.fa.min.js")
                .Include("~/Bundles/Vendors/KendoUI/js/messages/kendo.messages.fa-IR.min.js")
                .Include("~/Bundles/Vendors/Customs/js/kendo.frameworks.js")
                .Include("~/Bundles/Scripts/Frameworks/KendoFrameworks/datasource.kendo.js")
                .Include("~/Bundles/Scripts/Frameworks/KendoFrameworks/localdatasource.kendo.js")
                .Include("~/Bundles/Scripts/Frameworks/KendoFrameworks/grid.kendo.js")
                .Include("~/Bundles/Scripts/Frameworks/KendoFrameworks/combobox.kendo.js")
                .Include("~/Bundles/Scripts/Frameworks/KendoFrameworks/upload.kendo.js")
                .Include("~/Bundles/Scripts/Frameworks/KendoFrameworks/window.kendo.js")
                .Include("~/Bundles/Scripts/Frameworks/KendoFrameworks/numerictextbox.kendo.js")

                //  Service
                .Include("~/Bundles/Scripts/Frameworks/ajax.framework.js")
                .Include("~/Bundles/Scripts/Frameworks/navigate.framework.js")
                //.Include("~/Bundles/Scripts/Services/base.service.js")
                //.Include("~/Bundles/Scripts/Services/patient.service.js")
                //.Include("~/Bundles/Scripts/Services/document.service.js")
                //.Include("~/Bundles/Scripts/Services/home.service.js")
                //.Include("~/Bundles/Scripts/Services/documentSale.service.js")
                //.Include("~/Bundles/Scripts/Services/documentSaleProduct.service.js")
                //.Include("~/Bundles/Scripts/Services/doctor.service.js")
                //.Include("~/Bundles/Scripts/Services/product.service.js")
                //.Include("~/Bundles/Scripts/Services/productSeller.service.js")
                //.Include("~/Bundles/Scripts/Services/productBuy.service.js")
                //.Include("~/Bundles/Scripts/Services/presenter.service.js")
                //.Include("~/Bundles/Scripts/Services/report.service.js")
                //.Include("~/Bundles/Scripts/Services/service.js")
                .Include("~/Bundles/Scripts/Services/*.js")
                .Include("~/Bundles/Scripts/Services/Document/*.js")

                //  Custome
                .Include("~/Bundles/Scripts/Methods/ajax.methods.js")
                .Include("~/Bundles/Scripts/Methods/app.methods.js"));

            #endregion Bundles

            BundleTable.EnableOptimizations = false;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// </summary>
    internal class NonOrderingBundleOrderer : IBundleOrderer
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }

        #endregion Public Methods
    }
}