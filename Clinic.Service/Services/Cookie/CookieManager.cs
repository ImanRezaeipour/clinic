using System;
using System.Web;

namespace Clinic.Service.Services.Cookie
{
    public class CookieManager : ICookieManager
    {
        public bool SetCookie(string key, string value)
        {
            var cookie = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.Now.AddDays(7),
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            return true;
        }

        public bool SetCookieWithExpireTime(string key, string value, int day)
        {
            var cookie = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.Now.AddDays(day),
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            return true;
        }

        public string GetCookieValue(string key)
        {
            var cookie = System.Web.HttpContext.Current.Request.Cookies.Get(key);

            return cookie != null ? cookie.Value : string.Empty;
        }

        public HttpCookie GetCookie(string key)
        {
            var cookie = System.Web.HttpContext.Current.Request.Cookies.Get(key);

            return cookie;
        }

        public bool RemoveCookie(string key)
        {
            var cookie = System.Web.HttpContext.Current.Response.Cookies.Get(key);
            if (cookie != null) cookie.Expires = DateTime.Now.AddDays(-1);
            return true;
        }

        public bool SetCookieWithOutExpireTime(string key, string value)
        {
            var cookie = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.Now.AddYears(30)
            };
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            return true;
        }

    }
}
