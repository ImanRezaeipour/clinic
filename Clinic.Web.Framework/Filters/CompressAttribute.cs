using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace Clinic.FrameWork.Filters
{
    public class CompressAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// An oldie, but a goodie. Been using this routine for a while (Thanks, Kazi Manzur Rashid).
        ///
        ///If you want to compress your HTML when sending it down to the client, apply this compress filter to get the fastest package delivery available.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool allowCompression = false;
            bool.TryParse("true", out allowCompression);

            if (allowCompression)
            {
                HttpRequestBase request = filterContext.HttpContext.Request;

                string acceptEncoding = request.Headers["Accept-Encoding"];

                if (string.IsNullOrEmpty(acceptEncoding)) return;

                acceptEncoding = acceptEncoding.ToUpperInvariant();

                HttpResponseBase response = filterContext.HttpContext.Response;

                if (acceptEncoding.Contains("GZIP"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("DEFLATE"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
            }
        }
    }
}
