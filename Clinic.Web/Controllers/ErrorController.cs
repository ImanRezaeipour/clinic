using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    /// <summary>
    /// Provides methods that respond to HTTP requests with HTTP errors.
    /// </summary>
    public partial class ErrorController : BaseController
    {

        #region Public Methods

        /// <summary>
        /// Returns a HTTP 400 Bad Request error view. Returns a partial view if the request is an
        /// AJAX call.
        /// </summary>
        /// <returns> The partial or full bad request view. </returns>
        [AllowAnonymous]
        public virtual async Task<ActionResult> BadRequest()
        {
            //Response.StatusCode = (int)HttpStatusCode.BadRequest;
            // Don't show IIS custom errors.
            Response.TrySkipIisCustomErrors = true;
            return View(MVC.Error.Views.BadRequest);
        }

        /// <summary>
        /// Returns a HTTP 403 Forbidden error view. Returns a partial view if the request is an AJAX
        /// call. Unlike a 401 Unauthorized response, authenticating will make no difference.
        /// </summary>
        /// <returns> The partial or full forbidden view. </returns>
        [AllowAnonymous]
        public virtual async Task<ActionResult> Forbidden()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            // Don't show IIS custom errors.
            Response.TrySkipIisCustomErrors = true;
            return View(MVC.Error.Views.Forbidden);
        }

        /// <summary>
        /// Returns a HTTP 500 Internal Server Error error view. Returns a partial view if the
        /// request is an AJAX call.
        /// </summary>
        /// <returns> The partial or full internal server error view. </returns>
        [AllowAnonymous]
        public virtual async Task<ActionResult> InternalServerError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            // Don't show IIS custom errors.
            Response.TrySkipIisCustomErrors = true;
            return View(MVC.Error.Views.InternalServerError);
        }

        /// <summary>
        /// Returns a HTTP 405 Method Not Allowed error view. Returns a partial view if the request
        /// is an AJAX call.
        /// </summary>
        /// <returns> The partial or full method not allowed view. </returns>
        [AllowAnonymous]
        public virtual async Task<ActionResult> MethodNotAllowed()
        {
            Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            // Don't show IIS custom errors.
            Response.TrySkipIisCustomErrors = true;
            return View(MVC.Error.Views.MethodNotAllowed);
        }

        /// <summary>
        /// Returns a HTTP 404 Not Found error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns> The partial or full not found view. </returns>
        [AllowAnonymous]
        public virtual ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            // Don't show IIS custom errors.
            Response.TrySkipIisCustomErrors = true;
            return View(MVC.Error.Views.NotFound);
        }

        /// <summary>
        /// Returns a HTTP 401 Unauthorized error view. Returns a partial view if the request is an
        /// AJAX call.
        /// </summary>
        /// <returns> The partial or full unauthorized view. </returns>
        [AllowAnonymous]
        public virtual ActionResult Unauthorized()
        {
            //Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            // Don't show IIS custom errors.
            Response.TrySkipIisCustomErrors = true;
            return View(MVC.Error.Views.Unauthorized);
        }

        #endregion Public Methods

    }
}