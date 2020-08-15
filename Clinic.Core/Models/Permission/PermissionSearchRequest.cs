using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Permission
{
    public class PermissionSearchRequest : BaseSearchRequest
    {
        #region Public Properties

        public Guid? CreatedById { get; set; }
        public bool? IsCallback { get; set; }

        #endregion Public Properties
    }
}