using System;

namespace Clinic.Core.Models.User
{
    public class UserChangePasswordViewModel
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}