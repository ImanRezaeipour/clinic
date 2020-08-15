using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Clinic.Core.Types;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Clinic.Core.Domains.Users
{
    /// <summary>
    ///     نشان دهنده امنیت کاربر
    /// </summary>
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        #region Properties

        /// <summary>
        ///     آیا کاربر بلاک شده؟
        /// </summary>
        public virtual bool? IsBan { get; set; }

        /// <summary>
        ///     دلیل بلاک کاربر
        /// </summary>
        public virtual string BannedReason { get; set; }

        /// <summary>
        /// </summary>
        public virtual DateTime? BannedOn { get; set; }

        /// <summary>
        ///     آیا کاربر تائید شده است؟
        /// </summary>
        public virtual bool? IsVerify { get; set; }

        /// <summary>
        ///     آیا کاربر فعال است؟
        /// </summary>
        public virtual bool? IsActive { get; set; }

        /// <summary>
        ///     آیا کاربر مهمان است؟
        /// </summary>
        public virtual bool? IsAnonymous { get; set; }

        /// <summary>
        ///     توکن تائید فعال سازی ایمیل
        /// </summary>
        public virtual string EmailConfirmationToken { get; set; }

        /// <summary>
        ///     توکن تائید فغال سازی موبایل
        /// </summary>
        public virtual string MobileConfirmationToken { get; set; }

        /// <summary>
        ///     آخرین زمان تغییر پسورد
        /// </summary>
        public virtual DateTime? LastPasswordChangedOn { get; set; }

        /// <summary>
        ///     آخرین زمانی که کاربر وارد سایت شده
        /// </summary>
        public virtual DateTime? LastLoginedOn { get; set; }

        /// <summary>
        /// </summary>
        public virtual bool? IsSystemAccount { get; set; }

        /// <summary>
        /// </summary>
        public virtual string LastIp { get; set; }

        /// <summary>
        /// </summary>
        public virtual byte?[] RowVersion { get; set; }

        /// <summary>
        ///     نشان دهنده این است که آیا دسترسی های کاربر تغییر کرده است ؟
        /// </summary>
        public bool? IsChangePermission { get; set; }

        /// <summary>
        ///     دسترسی های مستقیم کاربر بدون وابستی به گروه های کاربری او
        /// </summary>
        [Column(TypeName = "xml")]
        public string DirectPermissions { get; set; }

        /// <summary>
        ///     ساختار اکس ام ال دسترسی های مستقیم کاربر بدون وابستی به گروه های کاربری او
        /// </summary>
        [NotMapped]
        public XElement XmlDirectPermissions
        {
            get { return XElement.Parse(DirectPermissions); }
            set { DirectPermissions = value.ToString(); }
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
        ///     لیست نظرات کاربر
        /// </summary>
        public virtual User ModifiedBy { get; set; }

        /// <summary>
        ///     به عنوان آخرین تغییر دهنده
        /// </summary>
        public virtual Guid? ModifiedById { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual User CreatedBy { get; set; }

        /// <summary>
        ///     به عنوان ایجاد کننده
        /// </summary>
        public virtual Guid? CreatedById { get; set; }


        #endregion 
    }
}