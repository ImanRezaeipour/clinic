using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Core.Domains.Users;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

namespace Clinic.Service.Services.Users
{
    public interface IUserService :IDisposable
    {
        #region Public Properties

        /// <summary>
        /// </summary>
        bool AutoCommitEnabled { get; set; }

        IClaimsIdentityFactory<User, Guid> ClaimsIdentityFactory { get; set; }
        TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
        IIdentityMessageService EmailService { get; set; }
        int MaxFailedAccessAttemptsBeforeLockout { get; set; }
        IPasswordHasher PasswordHasher { get; set; }
        IIdentityValidator<string> PasswordValidator { get; set; }
        IIdentityMessageService SmsService { get; set; }
        bool SupportsQueryableUsers { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserLockout { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserTwoFactor { get; }
        IDictionary<string, IUserTokenProvider<User, Guid>> TwoFactorProviders { get; }
        bool UserLockoutEnabledByDefault { get; set; }
        IQueryable<User> Users { get; }
        IUserTokenProvider<User, Guid> UserTokenProvider { get; set; }
        IIdentityValidator<User> UserValidator { get; set; }

        #endregion Public Properties

        #region Public Methods

        Task<IdentityResult> AccessFailedAsync(Guid userId);

        Task<IdentityResult> AddClaimAsync(Guid userId, Claim claim);

        Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login);

        Task<IdentityResult> AddPasswordAsync(Guid userId, string password);

        Task<IdentityResult> AddToRoleAsync(Guid userId, string role);

        Task<IdentityResult> AddToRolesAsync(Guid userId, params string[] roles);

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task BanByIdAsync(Guid userId);

        Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

        Task<IdentityResult> ChangePhoneNumberAsync(Guid userId, string phoneNumber, string token);

        Task<bool> CheckPasswordAsync(User user, string password);

        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token);


        Task<IdentityResult> CreateAsync(User user);

        Task<IdentityResult> CreateAsync(User user, string password);

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(UserCreateViewModel viewModel);

        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);

        /// <summary>
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> CustomValidatePasswordAsync(string password);

        Task<IdentityResult> DeleteAsync(User user);

        Task DeleteByIdAsync(Guid userId);

        void Dispose();

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<UserEditViewModel> FillForEditAsync(UserEditViewModel viewModel);

        Task<User> FindAsync(string userName, string password);

        Task<User> FindAsync(UserLoginInfo login);

        Task<User> FindByEmailAsync(string email);

        /// <summary>
        /// </summary>
        /// <param name="email">   </param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> FindByEmailPasswordAsync(string email, string password);

        Task<User> FindByIdAsync(Guid userId);

        Task<User> FindByNameAsync(string userName);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<User> FindCurrentUserAsync();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<User> FindSystemAccountAsync();

        Task<string> GenerateChangePhoneNumberTokenAsync(Guid userId, string phoneNumber);

        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);

        Task<string> GeneratePasswordResetTokenAsync(Guid userId);

        Task<string> GenerateTwoFactorTokenAsync(Guid userId, string twoFactorProvider);

        /// <summary>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ClaimsIdentity> GenerateUserIdentityAsync(User user);
        Task<string> GenerateUserTokenAsync(string purpose, Guid userId);

        Task<int> GetAccessFailedCountAsync(Guid userId);

        Task<IList<Claim>> GetClaimsAsync(Guid userId);

        Task<string> GetEmailAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        IUserEmailStore<User, Guid> GetEmailStore();

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserEditViewModel> GetForEditAsync(Guid userId);

        Task<bool> GetLockoutEnabledAsync(Guid userId);

        Task<DateTimeOffset> GetLockoutEndDateAsync(Guid userId);

        Task<IList<UserLoginInfo>> GetLoginsAsync(Guid userId);

        Task<string> GetPhoneNumberAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<string>> GetRolesAsync(Guid userId);

        Task<string> GetSecurityStampAsync(Guid userId);

        Task<bool> GetTwoFactorEnabledAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetUserNameAsSelectListItemAsync();

        Task<IList<string>> GetValidTwoFactorProvidersAsync(Guid userId);

        Task<bool> HasPasswordAsync(Guid userId);

        /// <summary>
        /// </summary>
        Task InitialUserServiceAsync();

        /// <summary>
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsBanByEmailAsync(string email);

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsBanByIdAsync(Guid userId);
        /// <summary>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsBanByUserNameAsync(string userName);

        Task<bool> IsEmailConfirmedAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <param name="email"> </param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsEmailExistAsync(string email, Guid? userId = null);

        Task<bool> IsInRoleAsync(Guid userId, string role);

        Task<bool> IsLockedOutAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsMustRefreshByIdAsync(Guid userId);

        Task<bool> IsPhoneNumberConfirmedAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsSystemAccountByIdAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<bool> IsSystemAccountExistAsync();

        /// <summary>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId">  </param>
        /// <returns></returns>
        Task<bool> IsUserNameExistAsync(string userName, Guid? userId);

        Task<IdentityResult> NotifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Func<CookieValidateIdentityContext, Task> OnValidateIdentity();

        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<User, Guid> provider);

        Task<IdentityResult> RemoveClaimAsync(Guid userId, Claim claim);

        Task<IdentityResult> RemoveFromRoleAsync(Guid userId, string role);

        Task<IdentityResult> RemoveFromRolesAsync(Guid userId, params string[] roles);

        Task<IdentityResult> RemoveLoginAsync(Guid userId, UserLoginInfo login);

        Task<IdentityResult> RemovePasswordAsync(Guid userId);

        Task<IdentityResult> ResetAccessFailedCountAsync(Guid userId);

        Task<IdentityResult> ResetPasswordAsync(Guid userId, string token, string newPassword);

       Task SendSmsAsync(Guid userId, string message);

        Task<IdentityResult> SetEmailAsync(Guid userId, string email);

        Task<IdentityResult> SetLockoutEnabledAsync(Guid userId, bool enabled);

        Task<IdentityResult> SetLockoutEndDateAsync(Guid userId, DateTimeOffset lockoutEnd);

        Task<IdentityResult> SetPhoneNumberAsync(Guid userId, string phoneNumber);

        Task<IdentityResult> SetTwoFactorEnabledAsync(Guid userId, bool enabled);

        Task<IdentityResult> UpdateAsync(User user);

        Task UpdateByViewModelAsync(UserEditViewModel viewModel);
        Task<IdentityResult> UpdateSecurityStampAsync(Guid userId);
        Task<bool> VerifyChangePhoneNumberTokenAsync(Guid userId, string token, string phoneNumber);

        Task<bool> VerifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token);

        Task<bool> VerifyUserTokenAsync(Guid userId, string purpose, string token);

        #endregion Public Methods
    }
}