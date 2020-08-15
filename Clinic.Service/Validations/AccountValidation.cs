using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Account;
using Clinic.Service.Services.Users;

namespace Clinic.Service.Validations
{
    /// <summary>
    ///
    /// </summary>
    public class AccountValidation : IAccountValidation
    {
        private readonly IUserService _userService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="userService"></param>
        public AccountValidation(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> SignInValidationAsync(LoginViewModel viewModel)
        {
            var user = await _userService.FindAsync(viewModel.UserName, viewModel.Password);
            if (user == null)
            {
                return "نام کاربری یا کامه عبور اشتباه است";
            }


            var isBan = await _userService.IsBanByUserNameAsync(viewModel.UserName);
            if (isBan == true)
            {
                return "حساب کاربری شما مسدود شده است";
            }

            

            var isLockedout = await _userService.IsLockedOutAsync(user.Id);
            if (isLockedout == true)
            {
                return "حساب کاربری شما قفل می باشد";
            }

            return null;
        }

        public Task<string> SignInAsync(LoginViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
