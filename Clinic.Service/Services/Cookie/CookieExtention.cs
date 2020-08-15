using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace Clinic.Service.Services.Cookie
{
    /// <summary>
    /// </summary>
    public static class CookieExtention
    {
        #region UpdateCookie

        /// <summary>
        /// </summary>
        /// <param name="httpContextBase"></param>
        /// <param name="cookieName"></param>
        /// <param name="value"></param>
        /// <param name="httpOnly"></param>
        public static void UpdateCookie(this HttpContextBase httpContextBase, string cookieName, string value,
            bool httpOnly = false)
        {
            var cookie = new HttpCookie(cookieName)
            {
                Value = httpContextBase.Server.UrlEncode(value),
                HttpOnly = httpOnly
            };
            httpContextBase.Response.Cookies.Set(cookie);
        }

        #endregion

        #region GetCookie

        /// <summary>
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookieValue(this HttpContextBase httpContext, string cookieName)
        {
            var cookie = httpContext.Request.Cookies[cookieName];
            return cookie == null ? string.Empty : httpContext.Server.UrlDecode(cookie.Value);
        }

        #endregion

        #region Deserialize

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base64EncodedSerializedTempData"></param>
        /// <returns></returns>
        public static T DeserializeCookie<T>(string base64EncodedSerializedTempData) where T : class
        {
            var bytes = Convert.FromBase64String(base64EncodedSerializedTempData);
            using (var memStream = new MemoryStream(bytes))
            {
                var binFormatter = new BinaryFormatter();
                return binFormatter.Deserialize(memStream, null) as T /*TempDataDictionary : This returns NULL*/;
            }
        }

        #endregion

        #region Serialize

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string SerializeToBase64EncodedString<T>(T values)
        {
            using (var memStream = new MemoryStream())
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var binFormatter = new BinaryFormatter();
                binFormatter.Serialize(memStream, values);
                memStream.Seek(0, SeekOrigin.Begin);
                var bytes = memStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }

        #endregion

        #region AddCookie

        /// <summary>
        /// </summary>
        /// <param name="httpContextBase"></param>
        /// <param name="cookieName"></param>
        /// <param name="value"></param>
        public static void AddCookie(this HttpContextBase httpContextBase, string cookieName, string value)
        {
            httpContextBase.AddCookie(cookieName, value, DateTime.Now.AddDays(30));
        }

        /// <summary>
        /// </summary>
        /// <param name="httpContextBase"></param>
        /// <param name="cookieName"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        /// <param name="httpOnly"></param>
        public static void AddCookie(this HttpContextBase httpContextBase, string cookieName, string value,
            DateTime expires, bool httpOnly = false)
        {
            var cookie = new HttpCookie(cookieName)
            {
                Expires = expires,
                Value = httpContextBase.Server.UrlEncode(value), // For Cookies and Unicode characters
                HttpOnly = httpOnly
            };
            httpContextBase.Response.Cookies.Add(cookie);
        }

        #endregion

        #region RemoveCookie

        /// <summary>
        /// </summary>
        /// <param name="httpContextBase"></param>
        /// <param name="cookieName"></param>
        public static void RemoveCookie(this HttpContextBase httpContextBase, string cookieName)
        {
            var cookie = new HttpCookie(cookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            httpContextBase.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// </summary>
        /// <param name="httpContextBase"></param>
        public static void RemoveAllCookies(this HttpContextBase httpContextBase)
        {
            var myCookies = httpContextBase.Request.Cookies.AllKeys;
            foreach (
                var httpCookie in
                    myCookies.Select(cookie => httpContextBase.Response.Cookies[cookie])
                        .Where(httpCookie => httpCookie != null))
            {
                httpCookie.Expires = DateTime.Now.AddDays(-1);
            }
        }

        #endregion
    }
}