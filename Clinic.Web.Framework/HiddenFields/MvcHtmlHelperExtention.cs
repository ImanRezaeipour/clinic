﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Clinic.Service.Services.Cookie;

namespace Clinic.FrameWork.HiddenFields
{
    /// <summary>
    /// </summary>
    public static class MvcHtmlHelperExtention
    {
        /// <summary>
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public static string GetActionKey(this RequestContext requestContext)
        {
            IActionKeyService actionKeyService = new ActionKeyService();
            var routeData = requestContext.RouteData;
            if (routeData.Values.ContainsKey("MS_DirectRouteMatches"))
            {
                routeData = ((IEnumerable<RouteData>) routeData.Values["MS_DirectRouteMatches"]).First();
            }
            var controller = routeData.Values["controller"]?.ToString();
            var area = routeData.Values["area"];
            var actionKeyValue =
                actionKeyService.GetActionKey(requestContext.HttpContext.GetCookieValue(AntiForgeryConfig.CookieName),
                    controller, area?.ToString());

            return actionKeyValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string GetActionKey(this HtmlHelper helper)
        {
            var routeData = helper.ViewContext.RouteData;
            if (routeData.Values.ContainsKey("MS_DirectRouteMatches"))
            {
                routeData = ((IEnumerable<RouteData>) routeData.Values["MS_DirectRouteMatches"]).First();
            }
            IActionKeyService actionKeyService = new ActionKeyService();
            var controller = routeData.Values["controller"].ToString();
            var area = routeData.Values["area"];
            var actionKeyValue =
                actionKeyService.GetActionKey(
                    helper.ViewContext.RequestContext.HttpContext.GetCookieValue(AntiForgeryConfig.CookieName),
                    controller, area?.ToString());

            return actionKeyValue;
        }
    }
}