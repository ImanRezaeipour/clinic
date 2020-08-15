﻿using System.Web;

namespace Clinic.Service.Services.Cookie
{
    public interface ICookieManager
    {
        HttpCookie GetCookie(string key);
        string GetCookieValue(string key);
        bool SetCookie(string key, string value);
        bool SetCookieWithExpireTime(string key, string value, int day);
    }
}