﻿using System;
using System.Net.Http;
using System.Web;

namespace Clinic.Core.Utilities.Http
{
    public static class HttpRequestMessageExtension
    {
        private const string HttpContext = "MS_HttpContext";

        public static Uri GetUrlReferrer(this HttpRequestMessage request)
        {
            if (!request.Properties.ContainsKey(HttpContext)) return null;
            var ctx = request.Properties[HttpContext] as HttpContextWrapper;
            return ctx?.Request.UrlReferrer;
        }
    }
}
