using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.User
{
    /// <summary>
    /// </summary>
    public class UserCreateViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        ///     کلمه عبور
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     تکرار کلمه عبور
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     نام کاربری
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     نام کاربر
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     نام خانوادگی کاربر
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     نام کاربر
        /// </summary>
        public string DisplayName { get; set; }

        #endregion
    }
}