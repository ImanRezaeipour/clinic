using System.IO;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    /// <summary>
    /// </summary>
    public partial class BaseController : System.Web.Mvc.Controller
    {
        #region Protected Methods

        ///// <summary>
        ///// </summary>
        ///// <param name="callback"></param>
        ///// <param name="state">   </param>
        ///// <returns></returns>
        //protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        //{
        //    //string cultureName = null;

        //    //// Attempt to read the culture cookie from Request
        //    //var cultureCookie = Request.Cookies["_culture"];
        //    //if (cultureCookie != null)
        //    //    cultureName = cultureCookie.Value;
        //    //else
        //    //    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
        //    //            Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
        //    //            null;
        //    //// Validate culture name
        //    //cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

        //    //// Modify current thread's cultures
        //    //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
        //    //Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        //    return base.BeginExecuteCore(callback, state);
        //}

        #endregion Protected Methods


        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        protected string RenderViewToString<T>(string viewPath, T model)
        {
            ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var view = new WebFormView(ControllerContext, viewPath);
                var vdd = new ViewDataDictionary<T>(model);
                var viewCxt = new ViewContext(ControllerContext, view, vdd,
                                            new TempDataDictionary(), writer);
                viewCxt.View.Render(viewCxt, writer);
                return writer.ToString();
            }
        }
    }
}