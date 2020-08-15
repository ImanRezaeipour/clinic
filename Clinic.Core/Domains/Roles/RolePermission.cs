using System;
using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Permissions;

namespace Clinic.Core.Domains.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public class RolePermission : BaseEntity
    {
        #region NavigationProperties

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid PermissionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Permission Permission { get; set; }

        #endregion
    }
}
