using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Core.Domains.Roles;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Role;

namespace Clinic.Service.Services.Roles
{
    public interface IRoleService
    {
        #region Public Properties

        /// <summary>
        /// </summary>
        bool AutoCommitEnabled { get; set; }

        #endregion Public Properties

        #region Public Methods

        Task<bool> IsSystemRoleAsync(Guid id);

        Task<int> CountByRequestAsync(RoleSearchRequest request);


        Task CreateByViewModelAsync(RoleCreateViewModel viewModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid roleId);


        Task EditByViewModelAsync(RoleEditViewModel viewModel);

        Task<Role> FindByUserIdAsync(Guid userId);


        Task<IList<string>> GetRoleNamesByUserIdAsync(Guid userId);

        Task<string> GenerateCodeAsync();


        Task<IList<SelectListItem>> GetRolesAsSelectListAsync();

        Task<IList<Role>> GetRolesByRequestAsync(RoleSearchRequest request);

        Task<IList<Role>> GetRolesByUserIdAsync(Guid userId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsExistNameAsync(string name, CancellationToken cancellationToken = default (CancellationToken));

        Task<string> MaxByRequestAsync(RoleSearchRequest request, string aggregateMember);

        #endregion Public Methods


        IQueryable<Role> QueryByRequest(RoleSearchRequest request);

        Task<Role> FindAsync(Guid roleId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<string>> GetPermissionNamesByUserIdAsync(Guid userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        Task<IList<Role>> GetRolesByPermissionIdAsync(Guid permissionId);
    }
}