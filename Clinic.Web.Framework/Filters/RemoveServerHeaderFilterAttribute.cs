using System.Web.Mvc;

namespace Clinic.FrameWork.Filters
{
    /// <summary>
    /// </summary>
    public class RemoveServerHeaderFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            // for prevent attack
            response.Headers.Remove("Server");
            base.OnActionExecuting(filterContext);
        }
    }
}