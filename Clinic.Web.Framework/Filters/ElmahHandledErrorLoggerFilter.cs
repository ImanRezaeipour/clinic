using System.Web.Mvc;
using Elmah;

namespace Clinic.FrameWork.Filters
{
    /// <summary>
    /// </summary>
    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        /// <summary>
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
        }
    }
}