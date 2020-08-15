using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Role
{
    public class RoleSearchRequest : BaseSearchRequest
    {
        #region Public Properties

        public Guid? CreatedById { get; set; }

        #endregion Public Properties
    }
}