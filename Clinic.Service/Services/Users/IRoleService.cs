using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Domains.Roles;
using Clinic.Core.Domains.Users;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Role;

namespace Clinic.Service.Services.Users
{
    public interface IRoleService
    {

        #region Public Properties

        /// <summary>
        /// </summary>
        bool AutoCommitEnabled { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> AnyByNameAsync(string roleName);

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(RoleCreateViewModel viewModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModels"></param>
        Task CreateRangeByViewModelAsync(IEnumerable<RoleCreateViewModel> viewModels);

        Task DeleteByIdAsync(Guid roleId);

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        Task<RoleEditViewModel> FillForEditAsync(RoleEditViewModel viewModel);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetAllAsSelectListAsync();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<IList<Role>> GetAllAsync();

        Task<IList<SelectListItem>> GetAsSelectListItemAsync();

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<RoleEditViewModel> GetForEditAsync(Guid roleId);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<IList<PermissionViewModel>> GetPermissionsAsync();

        Task<IList<PermissionViewModel>> GetPermissionsByRoleIdAsync(Guid roleId);

        Task<IList<string>> GetPermissionsByUserIdAsync(Guid userId);

        Task<IList<string>> GetRoleNameByUserIdAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string[]> GetRolesByUserIdAsync(Guid userId);

        Task<IList<Role>> GetRolesWithPermisionsByUserIdAsync(Guid userId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<Guid>> GetUserRoleIdByUserIdAsync(Guid userId);
        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<string>> GetUserRoleNameByUserIdAsync(Guid userId);

        /// <summary>
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<IList<UserRole>> GetUsersByRoleNameAsync(string roleName);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsSystemRoleByIdAsync(Guid id);

        /// <summary>
        /// </summary>
        /// <param name="userId">  </param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> IsUserInRoleAsync(Guid userId, string roleName);

        /// <summary>
        ///
        /// </summary>
        Task SeedAsync();

        Task<IList<RoleViewModel>> TreeRoleAsync();

        Task UpdateByViewModelAsync(RoleEditViewModel viewModel);

        #endregion Public Methods
    }
}