using System;
using Clinic.Core.Utilities.Http;
using Microsoft.AspNet.Identity;
using Clinic.Service.Services.Permissions;

namespace Clinic.Service.Services.HttpContext
{
    public class HttpContextManager : IHttpContextManager
    {
        /// <summary>
        ///
        /// </summary>
        public Guid CurrentUserId()
        {
            return Guid.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        /// <summary>
        ///
        /// </summary>
        public string CurrentRequestIp()
        {
            return System.Web.HttpContext.Current.Request.GetIp();
        }

        /// <summary>
        ///
        /// </summary>
        public string CurrentRequestBrowser()
        {
            return System.Web.HttpContext.Current.Request.GetBrowser();
        }

        /// <summary>
        ///
        /// </summary>
        public Uri CurrentRequestUrl()
        {
            return System.Web.HttpContext.Current.Request.Url;
        }

        /// <summary>
        ///
        /// </summary>
        public Guid CurrentRoleId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        public Guid CurrentCompanyId()
        {
            throw new NotImplementedException();
        }
    }
}
