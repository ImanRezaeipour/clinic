using System;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Validations.Common
{
    public abstract class BaseValidation
    {
        /// <summary>
        ///
        /// </summary>
        public Guid CurrentUserId => Guid.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId());
    }
}
