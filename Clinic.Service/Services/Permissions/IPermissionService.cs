using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Core.Domains.Permissions;
using Clinic.Core.Models.Permission;
using Clinic.Core.Objects;

namespace Advertise.Service.Services.Permissions
{
    public interface IPermissionService
    {
        Task<IList<PermissionViewModel>> GetAllPermissionsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<Permission>> GetPermissionsByRoleIdAsync(Guid roleId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IQueryable<Permission> QueryByRequest(PermissionSearchRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> CountByRequestAsync(PermissionSearchRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IList<Permission>> GetByRequestAsync(PermissionSearchRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        Task<Permission> FindByIdAsync(Guid permissionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid permissionId);


        Task EditByViewModelAsync(PermissionEditViewModel viewModel);


        Task CreateByViewModelAsync(PermissionCreateViewModel viewModel);

        Task<object> GetAllTreeAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        Task<IList<Guid>> GetIdsByNamesAsync(IList<string> names);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<JsTreeObject>> GetAllTreeByRoleIdAsync(Guid roleId);
    }
}