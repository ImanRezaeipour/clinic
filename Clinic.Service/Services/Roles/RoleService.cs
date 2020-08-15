using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Advertise.Service.Services.Permissions;
using AutoMapper;
using Clinic.Core.Configuration;
using Clinic.Core.Domains.Roles;
using Clinic.Core.Extensions;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Role;
using Clinic.Data.DbContexts;
using Clinic.Service.Managers.Events;
using Clinic.Service.Services.Common;
using Clinic.Service.Services.HttpContext;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Services.Roles
{
    /// <inheritdoc cref="RoleManager{TRole,TKey}" />

    public class RoleService : RoleManager<Role, Guid>, IRoleService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IPermissionService _permissionService;
        private readonly IDbSet<RolePermission> _rolePermissionRepository;
        private readonly IDbSet<Role> _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="unitOfWork"></param>
        ///  <param name="mapper"></param>
        ///  <param name="roleStore"></param>
        ///  <param name="eventPublisher"></param>
        /// <param name="permissionService"></param>
        /// <param name="permissionService1"></param>
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IRoleStore<Role, Guid> roleStore, IEventPublisher eventPublisher, IPermissionService permissionService, IPermissionService permissionService1, IHttpContextManager httpContextManager)
            : base(roleStore)
        {
            _roleRepository = unitOfWork.Set<Role>();
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _permissionService = permissionService;
            _permissionService = permissionService1;
            _httpContextManager = httpContextManager;
            _rolePermissionRepository = unitOfWork.Set<RolePermission>();
            _mapper = mapper;
            AutoCommitEnabled = true;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public bool AutoCommitEnabled { get; set; }

        #endregion Public Properties

        #region Public Methods


        public async Task<int> CountByRequestAsync(RoleSearchRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var roles = await QueryByRequest(request).CountAsync();

            return roles;
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="viewModel"></param>
        ///  <returns></returns>
      public async Task CreateByViewModelAsync(RoleCreateViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var role = _mapper.Map<Role>(viewModel);
            role.Code = await GenerateCodeAsync();

            var permissionIds = viewModel.Permissions.Split(',');
            role.RolePermissions = new HashSet<RolePermission>();
            permissionIds.ForEach(permissionId => role.RolePermissions.Add(new RolePermission {PermissionId = permissionId.ToGuidOrDefault()}));
            role.CreatedById = _httpContextManager.CurrentUserId();
            role.CreatedOn = DateTime.Now;
            _roleRepository.Add(role);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(role);
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="roleId"></param>
        ///  <returns></returns>
        public async Task DeleteByIdAsync(Guid roleId)
        {
            if (roleId == null)
                throw new ArgumentNullException(nameof(roleId));

            var role = await FindByIdAsync(roleId);
            _roleRepository.Remove(role);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(role);
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="viewModel"></param>
        ///  <returns></returns>
        public async Task EditByViewModelAsync(RoleEditViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var role = await _roleRepository.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, role);

            var permissionIds = viewModel.Permissions.Split(',');
            role.RolePermissions = new HashSet<RolePermission>();
            permissionIds.ForEach(permissionId => role.RolePermissions.Add(new RolePermission { PermissionId = permissionId.ToGuidOrDefault() }));

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(role);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<Role> FindAsync(Guid roleId)
        {
            return await _roleRepository.FirstOrDefaultAsync(model => model.Id == roleId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Role> FindByUserIdAsync(Guid userId)
        {
            var query = from role in Roles
                        from user in role.Users
                        where user.UserId == userId
                        select role;

            return await query.FirstOrDefaultAsync();
        }


        public async Task<string> GenerateCodeAsync()
        {
            var request = new RoleSearchRequest();
            var maxCode = await MaxByRequestAsync(request, RoleAggregateMember.Code);
            return maxCode == null ? CodeConst.BeginNumber5Digit : maxCode.ExtractNumeric();
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="userId"></param>
        ///  <returns></returns>
        public async Task<IList<string>> GetPermissionNamesByUserIdAsync(Guid userId)
        {
            var permissionNames = new List<string>();
            var userRoles = await GetRolesByUserIdAsync(userId);
            foreach (var role in userRoles)
            {
                permissionNames.Add(role.Name);
                var rolePermissions = await _permissionService.GetPermissionsByRoleIdAsync(role.Id);
                rolePermissions.ForEach(permission => permissionNames.Add(permission.Name));
            }
            return permissionNames;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetRoleNamesByUserIdAsync(Guid userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var result = await userRolesQuery
                .OrderBy(model => model.Name)
                .Select(model => model.Name)
                .ToListAsync();

            return result;
        }


        public async Task<IList<SelectListItem>> GetRolesAsSelectListAsync()
        {
            var roles = await _roleRepository
                .AsNoTracking()
                .Select(record => new SelectListItem
                {
                    Value = record.Id.ToString(),
                    Text = record.Name
                })
                .ToListAsync();

            return roles;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public async Task<IList<Role>> GetRolesByPermissionIdAsync(Guid permissionId)
        {
            var roleIds = await _rolePermissionRepository.AsNoTracking()
                .Where(model => model.PermissionId == permissionId)
                .Select(model => model.RoleId)
                .ToListAsync();

            var roles = await _roleRepository.AsNoTracking()
                .Where(model => roleIds.Contains(model.Id))
                .ToListAsync();

            return roles;
        }


        public async Task<IList<Role>> GetRolesByRequestAsync(RoleSearchRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<Role>> GetRolesByUserIdAsync(Guid userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var roles = await userRolesQuery.AsNoTracking()
                .ToListAsync();

            return roles;
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="roleName"></param>
        ///  <returns></returns>
        public async Task<bool> IsExistNameAsync(string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _roleRepository.AsNoTracking().AnyAsync(model => model.Name == roleName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<bool> IsSystemRoleAsync(Guid roleId)
        {
            if (roleId == null)
                throw new ArgumentNullException(nameof(roleId));

            return await _roleRepository.AsNoTracking()
                .AnyAsync(model => model.Id == roleId && model.IsSystemRole == true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="aggregateMember"></param>
        /// <returns></returns>
        public async Task<string> MaxByRequestAsync(RoleSearchRequest request, string aggregateMember)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var products = QueryByRequest(request);
            switch (aggregateMember)
            {
                case RoleAggregateMember.Code:
                    var memberMax = await products.MaxAsync(model => model.Code);
                    return memberMax;
            }

            return null;
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="request"></param>
        ///  <returns></returns>
        public IQueryable<Role> QueryByRequest(RoleSearchRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var roles = _roleRepository.AsNoTracking().AsQueryable();

            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;

            roles = roles.OrderBy($"{request.SortMember} {request.SortDirection}");

            return roles;
        }

        #endregion Public Methods
    }
}