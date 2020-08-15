using System;
using Clinic.Core.Domains.Addresses;

namespace Clinic.Core.Domains.Common
{
    /// <summary>
    ///     نشان دهنده موجودیت پایه
    /// </summary>
    public abstract class BasePerson : BaseEntity
    {
        #region Public Properties

        /// <summary>   آدرس </summary>
        /// <value> The address. </value>
        public virtual Address Address { get; set; }

        /// <summary>   شناسه آدرس </summary>
        /// <value> The identifier of the address. </value>
        public virtual Guid? AddressId { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// فعال
        /// </summary>
        public virtual bool? IsActive { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// تلفن همراه
        /// </summary>
        public virtual string MobileNumber { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        #endregion Public Properties
    }
}