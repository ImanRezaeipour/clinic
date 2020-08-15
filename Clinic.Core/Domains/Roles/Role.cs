using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Clinic.Core.Domains.Users;
using Clinic.Core.Types;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Clinic.Core.Domains.Roles
{

    public class Role : IdentityRole<Guid, UserRole>
    {
        #region Properties

        /// <summary>
        ///     کد نقش
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// </summary>
        [Timestamp]
        public virtual byte?[] RowVersion { get; set; }

        /// <summary>
        /// </summary>
        public virtual bool? IsSystemRole { get; set; }

        /// <summary>
        ///     آیا حساب  کاربران این گروه کاربری مسدود شود؟
        /// </summary>
        public virtual bool? IsBan { get; set; }

        /// <summary>
        ///     لیست دسترسی های گروه کاربری
        /// </summary>
        [Column(TypeName = "xml")]
        public virtual string Permissions { get; set; }

        /// <summary>
        ///     فرمت ایکس ام ال دسترسی ها
        /// </summary>
        [NotMapped]
        public virtual XElement XmlPermissions
        {
            get { return XElement.Parse(Permissions); }
            set { Permissions = value.ToString(); }
        }

        /// <summary>
        ///     gets or sets date that this entity was created
        /// </summary>
        public virtual DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     gets or sets Date that this entity was updated
        /// </summary>
        public virtual DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     gets or sets IP Address of Creator
        /// </summary>
        public virtual string CreatorIp { get; set; }

        /// <summary>
        ///     gets or set IP Address of Modifier
        /// </summary>
        public virtual string ModifierIp { get; set; }

        /// <summary>
        ///     به منظور ممانعت از ویرایش
        /// </summary>
        public virtual bool? IsModifyLock { get; set; }

        /// <summary>
        ///     indicate this entity is deleted softly
        /// </summary>
        public virtual bool? IsDelete { get; set; }

        /// <summary>
        ///     gets or sets information of user agent of modifier
        /// </summary>
        public virtual string ModifierAgent { get; set; }

        /// <summary>
        ///     gets or sets information of user agent of Creator
        /// </summary>
        public virtual string CreatorAgent { get; set; }

        /// <summary>
        ///     gets or sets count of Modification Default is 1
        /// </summary>
        public virtual int? Version { get; set; }

        /// <summary>
        ///     gets or sets action (create,update,softDelete)
        /// </summary>
        public virtual AuditType? Audit { get; set; }

        #endregion


        #region NavigationProperties

        /// <summary>
        ///     به عنوان آخرین تغییر دهنده
        /// </summary>
       // public virtual User ModifiedBy { get; set; }

        /// <summary>
        ///     به عنوان آخرین تغییر دهنده
        /// </summary>
       // public virtual Guid? ModifiedById { get; set; }

        /// <summary>
        ///     به عنوان ایجاد کننده
        /// </summary>
        public virtual User CreatedBy { get; set; }

        /// <summary>
        ///     به عنوان ایجاد کننده
        /// </summary>
        public virtual Guid? CreatedById { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}