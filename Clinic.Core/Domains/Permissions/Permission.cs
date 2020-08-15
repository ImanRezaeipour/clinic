using System;
using System.Collections.Generic;
using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Roles;

namespace Clinic.Core.Domains.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public class Permission : BaseEntity
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string MethodName { get; set; }

        /// <summary>
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int? Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool? IsPermission { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool? IsCallback { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual Permission Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
