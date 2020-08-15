using System;
using System.Linq;
using System.Web;

namespace Clinic.Core.Utilities.Http
{
    /// <summary>
    /// </summary>
    public static class HttpExtention
    {
        #region PhysicalToVirtualPathConverter

        /// <summary>
        /// </summary>
        /// <param name="utility"></param>
        /// <param name="path"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string PhysicalToVirtualPathConverter(this HttpServerUtilityBase utility, string path,
            HttpRequestBase context)
        {
            return path.Replace(context.ServerVariables["APPL_PHYSICAL_PATH"], "/").Replace(@"\", "/");
        }

        #endregion

        #region User-Agent

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetBrowser(this HttpRequestBase request)
        {
            return $"{request.Browser.Browser} - {request.Browser.Version}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetBrowser(this HttpRequest request)
        {
            return $"{request.Browser.Browser} - {request.Browser.Version}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetOs(this HttpRequestBase request)
        {
            var browserDetails = request.UserAgent;

            try
            {
                if (browserDetails != null)
                    switch (browserDetails.Substring(browserDetails.LastIndexOf("Windows NT", StringComparison.Ordinal) + 11, 3).Trim())
                    {
                        case "6.2":
                            return "Windows 8";
                        case "6.1":
                            return "Windows 7";
                        case "6.0":
                            return "Windows Vista";
                        case "5.2":
                            return "Windows XP 64-Bit Edition";
                        case "5.1":
                            return "Windows XP";
                        case "5.0":
                            return "Windows 2000";
                        default:
                            return browserDetails.Substring(browserDetails.LastIndexOf("Windows NT", StringComparison.Ordinal), 14);
                    }
            }
            catch
            {
                if (browserDetails != null && browserDetails.Length > 149)
                    return browserDetails.Substring(0, 149);
                else
                    return browserDetails;
            }

            return null;
        }

        #endregion

        #region GetIpAddress

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetIp(this HttpRequestBase request)
        {
            string ip = null;
            try
            {
                if (request.IsSecureConnection)
                {
                    ip = request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.IsNullOrEmpty(ip))
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ip))
                    {
                        if (ip.IndexOf(",", StringComparison.Ordinal) > 0)
                        {
                            ip = ip.Split(',').Last();
                        }
                    }
                    else
                    {
                        ip = request.UserHostAddress;
                    }
                }
            }
            catch
            {
                ip = null;
            }

            return ip;
        }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetIp(this HttpRequest request)
        {
            string ip = null;
            try
            {
                if (request.IsSecureConnection)
                {
                    ip = request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.IsNullOrEmpty(ip))
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ip))
                    {
                        if (ip.IndexOf(",", StringComparison.Ordinal) > 0)
                        {
                            ip = ip.Split(',').Last();
                        }
                    }
                    else
                    {
                        ip = request.UserHostAddress;
                    }
                }
            }
            catch
            {
                ip = null;
            }

            return ip;
        }

        #endregion
    }
}