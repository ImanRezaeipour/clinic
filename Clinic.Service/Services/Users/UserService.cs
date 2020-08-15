using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Configuration;
using Clinic.Core.Domains.Roles;
using Clinic.Core.Domains.Users;
using Clinic.Core.Extensions;
using Clinic.Core.Helpers;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.User;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using Clinic.Service.Services.Roles;
using Clinic.Service.Services.Users.Factories;
using Clinic.Service.Validations.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;

namespace Clinic.Service.Services.Users
{
    /// <summary>
    /// </summary>
    public class UserService : UserManager<User, Guid>, IUserService
    {

        #region Private Fields

        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _users;


        #endregion Private Fields

        // private readonly IApplicationSignInManager _applicationSignInManager;

        #region Public Constructors

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="mapper"></param>
        ///  <param name="applicationPermissionService"></param>
        ///  <param name="roleService"></param>
        ///  <param name="userStore"></param>
        ///  <param name="identity"></param>
        ///  <param name="dataProtectionProvider"></param>
        ///  <param name="smsService"></param>
        ///  <param name="emailService"></param>
        ///  <param name="unitOfWork"></param>
        /// <param name="listManager"></param>
        /// <param name="httpContextManager"></param>
        /// <param name="configurationManager"></param>
        public UserService(IMapper mapper,IUserStore<User, Guid> userStore, IDataProtectionProvider dataProtectionProvider, IIdentityMessageService smsService, IIdentityMessageService emailService, IRoleService roleService, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager) : base(userStore)
        {
            _mapper = mapper;
            _dataProtectionProvider = dataProtectionProvider;
            _users = unitOfWork.Set<User>();
            _roleService = roleService;
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            SmsService = smsService;
            EmailService = emailService;
            AutoCommitEnabled = true;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// </summary>
        public bool AutoCommitEnabled { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="user">   </param>
        /// <returns></returns>
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserService service, User user)
        {
            // Process
            var result = await service.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            //  Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task BanByIdAsync(Guid userId)
        {
            // Process
            var user = await _users.FirstOrDefaultAsync(model => model.Id == userId);
            user.IsBan = true;
           await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
            await UpdateSecurityStampAsync(userId);

          
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(UserCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var user = _mapper.Map<User>(viewModel);
            await CreateAsync(user, viewModel.Password);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> CustomValidatePasswordAsync(string password)
        {
            // Process
            var result = await PasswordValidator.ValidateAsync(password);

            //  Result
            return result.Errors.GetUserManagerErros();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid userId)
        {
            // Process
            var user = await _users.FirstOrDefaultAsync(model => model.Id == userId);
            _users.Remove(user);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<UserEditViewModel> FillForEditAsync(UserEditViewModel viewModel)
        {
            //  Process
            viewModel.RoleList = await _roleService.GetRolesAsSelectListAsync();
            var role = await _users.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            if (role != null)
                viewModel.RoleId = role.Id;

            //  Result
            return viewModel;
        }

        /// <summary>
        /// </summary>
        /// <param name="email">   </param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> FindByEmailPasswordAsync(string email, string password)
        {
            // Process
            var user = await FindByEmailAsync(email);

            // Result
            return await CheckPasswordAsync(user, password) ? user : null;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<User> FindCurrentUserAsync()
        {
            // Process
            var result = await FindByIdAsync(_httpContextManager.CurrentUserId());

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<User> FindSystemAccountAsync()
        {
            // Process
            var user = await _users.FirstOrDefaultAsync(model => model.IsSystemAccount == true);

            // Result
            return user;
        }

        /// <summary>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user)
        {
            //  Process
            var result = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IUserEmailStore<User, Guid> GetEmailStore()
        {
            // Process
            var cast = Store as IUserEmailStore<User, Guid>;
            if (cast == null)
            {
                throw new NotSupportedException("not support");
            }

            // Result
            return cast;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserEditViewModel> GetForEditAsync(Guid userId)
        {
            // Process
            var user = await _users.FirstOrDefaultAsync(model => model.Id == userId);
            var viewModel = _mapper.Map<UserEditViewModel>(user);

            await FillForEditAsync(viewModel);

            // Result
            return viewModel;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override async Task<IList<string>> GetRolesAsync(Guid userId)
        {
            // Process
            var userPermissions = await _roleService.GetPermissionNamesByUserIdAsync(userId);

            // Result
            return userPermissions;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetUserNameAsSelectListItemAsync()
        {
            // Process
            var result = await _users.Select(user => new SelectListItem { Text = user.UserName, Value = user.Id.ToString() }).ToListAsync();

            //  Result
            return result;
        }

        /// <summary>
        /// </summary>
        public async Task InitialUserServiceAsync()
        {
            // Process
            ClaimsIdentityFactory = new ApplicationClaimsIdentityFactory();
            UserValidator = new ApplicationUserValidator<User, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };
            PasswordValidator = new ApplicationPasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;
            if (_dataProtectionProvider == null) return;
            var dataProtector = _dataProtectionProvider.Create("Application Identity");
            UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(dataProtector);
        }

        /// <summary>
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> IsBanByEmailAsync(string email)
        {
            // Process
            var result = await _users.AnyAsync(user => user.Email == email.ToLower() && user.IsBan == true);

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsBanByIdAsync(Guid userId)
        {
            // Process
            var result = await _users.AnyAsync(user => user.Id == userId && user.IsBan == true);

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsBanByUserNameAsync(string userName)
        {
            //  Process
            var result = await _users.AnyAsync(user => user.UserName == userName.ToLower() && user.IsBan == true);

            //  Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="email"> </param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsEmailExistAsync(string email, Guid? userId = null)
        {
            // Process
            var result = userId == null
                 ? await _users.AnyAsync(user => user.Email == email.ToLower())
                 : await _users.AnyAsync(user => user.Email == email.ToLower() && user.Id != userId.Value);

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsMustRefreshByIdAsync(Guid userId)
        {
            // Process
            var user = await _users.FirstOrDefaultAsync(model => model.Id == userId);
            var result = user.IsChangePermission.GetValueOrDefault() || user.IsBan.GetValueOrDefault();

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsSystemAccountByIdAsync(Guid userId)
        {
            // Process
            var result = await _users.AnyAsync(user => user.Id == userId && user.IsSystemAccount == true);

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsSystemAccountExistAsync()
        {
            // Process
            var result = await _users.AnyAsync(user => user.IsSystemAccount == true);

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId">  </param>
        /// <returns></returns>
        public async Task<bool> IsUserNameExistAsync(string userName, Guid? userId)
        {
            // Process
            var result = userId == null
                ? await _users.AnyAsync(a => string.Equals(a.UserName, userName, StringComparison.InvariantCultureIgnoreCase))
                : await _users.AnyAsync(a => string.Equals(a.UserName, userName, StringComparison.InvariantCultureIgnoreCase) && a.Id != userId.Value);

            // Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            // Process
            var result = ApplicationSecurityStampValidator.OnValidateIdentity(TimeSpan.FromMinutes(0), GenerateUserIdentityAsync, identity => Guid.Parse(identity.GetUserId()));

            // Result
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        //public async Task SeedAsync()
        //{
        //    var adminUser = await _users.FirstOrDefaultAsync(user => user.IsSystemAccount == true);
        //    if (adminUser == null)
        //    {
        //        adminUser = new User
        //        {
        //            Id = SequentialGuidGenerator.NewSequentialGuid(),
        //            UserName = _configurationManager.AdminUserName,
        //            IsSystemAccount = true,
        //            Email = _configurationManager.AdminEmail,
        //            IsActive = true
        //        };
        //        this.Create(adminUser, _configurationManager.AdminPassword);
        //        this.SetLockoutEnabled(adminUser.Id, false);
        //    }
        //    var userRoles = await _roleService.GetUserRoleNameByUserIdAsync(adminUser.Id);
        //    if (userRoles.Any(role => role == RolePermissionConst.Administrators))
        //        return;
        //    this.AddToRole(adminUser.Id, RolePermissionConst.Administrators);
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(UserEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var user = await _users.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            var updated = _mapper.Map(viewModel, user);

            var roles = _mapper.Map<UserRole>(viewModel.Role);
            updated.Roles.Clear();
            updated.Roles.Add(roles);

           await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

            this.UpdateSecurityStamp(user.Id);
        }

        #endregion Public Methods

    }
}