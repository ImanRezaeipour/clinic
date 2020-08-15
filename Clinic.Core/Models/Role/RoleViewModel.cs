using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Role
{
    /// <summary>
    ///     ویو مدل نمایش گروه کاربری
    /// </summary>
    public class RoleViewModel : BaseViewModel
    {
        #region Public Properties

        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     آی دی
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     آیا کاربران این گروه قفل شوند؟
        /// </summary>
        public bool IsBan { get; set; }

        /// <summary>
        ///     آیا گروه سیستمی است؟
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        ///     نام
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     لیست دسترسی های گروه کاربری
        /// </summary>
        public string Permissions { get; set; }

        #endregion Public Properties
    }
}