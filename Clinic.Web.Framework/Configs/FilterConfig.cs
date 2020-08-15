using System.Web.Mvc;
using Clinic.FrameWork.Filters;

namespace Clinic.FrameWork.Configs
{
    /// <summary>
    /// </summary>
    public static class FilterConfig
    {
        /// <summary>
        /// </summary>
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            // logg action errors
            filters.Add(new ElmahHandledErrorLoggerFilter());

            //  logg xss attacks
            filters.Add(new ElmahRequestValidationErrorFilter());

            //
            filters.Add(new RemoveServerHeaderFilterAttribute());

            //
            //filters.Add(new ForceWwwAttribute("http://localhost:25890/"));
        }
    }
}