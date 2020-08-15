using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.User
{
    /// <summary>
    /// </summary>
    public class UserViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        ///     آی دی
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     نام کاربری
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     قفل شده؟
        /// </summary>
        public bool IsBan { get; set; }

        /// <summary>
        ///     اکانت سیستمی است؟
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        ///     نام /نام خانوادگی
        /// </summary>
        public string DisplayName { get; set; }



        #endregion
    }
}