using System;
using System.Collections.Generic;
using Clinic.Core.Domains.Users;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.User
{
    public class UserEditViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        ///     نام کاربر
        /// </summary>
        public string DisplayName { get; set; }

        public string BannedReason { get; set; }

        public Guid Id { get; set; }

        public bool IsBan { get; set; }

        public bool IsActive { get; set; }

        public bool IsSystemAccount { get; set; }

        public Guid RoleId { get; set; }
        
        public IEnumerable<SelectListItem> RoleList { get; set; }
       // public IList<UserRoleNewViewModel> Roles { get; set; }

        public UserRole Role { get; set; }

        #endregion Public Properties
    }
}