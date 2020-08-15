using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Role
{

    public class RoleCreateViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        ///     نام گروه کاربری
        /// </summary>
        public string Name { get; set; }

        public string Permissions { get; set; }

        #endregion Public Properties
    }
}