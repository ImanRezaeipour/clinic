using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Service.Services.Permissions;
using AutoMapper.QueryableExtensions;
using Clinic.Core.Domains.Permissions;
using Clinic.Core.Domains.Roles;
using Clinic.Core.Exceptions;
using Clinic.Core.Extensions;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Permission;
using Clinic.Core.Objects;
using Clinic.Data.DbContexts;
using Clinic.Service.Managers.Events;

namespace Clinic.Service.Services.Permissions
{

    public class PermissionService : IPermissionService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Permission> _permissionRepository;
        private readonly IDbSet<RolePermission> _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="eventPublisher"></param>
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _permissionRepository = unitOfWork.Set<Permission>();
            _rolePermissionRepository = unitOfWork.Set<RolePermission>();
        }

        #endregion Public Constructors

        #region Public Methods


        public async Task<int> CountByRequestAsync(PermissionSearchRequest request)
        {
            return await QueryByRequest(request).CountAsync();
        }


        public async Task CreateByViewModelAsync(PermissionCreateViewModel viewModel)
        {
            var permission = _mapper.Map<Permission>(viewModel);
            _permissionRepository.Add(permission);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(permission);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid permissionId)
        {
            var permission = await FindByIdAsync(permissionId);
            _permissionRepository.Remove(permission);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(permission);
        }


        public async Task EditByViewModelAsync(PermissionEditViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var permission = await FindByIdAsync(viewModel.Id);
            if (permission == null)
                throw new ServiceException();
            _mapper.Map(viewModel, permission);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(permission);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public async Task<Permission> FindByIdAsync(Guid permissionId)
        {
            return await _permissionRepository.FirstOrDefaultAsync(model => model.Id == permissionId);
        }


        public async Task<IList<PermissionViewModel>> GetAllPermissionsAsync()
        {
            var request = new PermissionSearchRequest
            {
                PageSize = PageSize.All
            };
            var permissions = await GetByRequestAsync(request);
            var permissionsviewModel = _mapper.Map<IList<PermissionViewModel>>(permissions);

            return permissionsviewModel;
        }


        public async Task<IList<Permission>> GetByRequestAsync(PermissionSearchRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IList<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
        {
            var permissionIds = await _rolePermissionRepository.AsNoTracking()
                .Where(model => model.RoleId == roleId)
                .Select(model => model.PermissionId)
                .ToListAsync();

            var permissions = await _permissionRepository.AsNoTracking()
                .Where(model => permissionIds.Contains(model.Id))
                .ToListAsync();

            return permissions;
        }


        public IQueryable<Permission> QueryByRequest(PermissionSearchRequest request)
        {
            var permissions = _permissionRepository.AsNoTracking().AsQueryable();

            permissions = permissions.Where(model => model.Name != null);
            if (request.IsCallback != null)
                permissions = permissions.Where(model => model.IsCallback == request.IsCallback);

            if (request.SortMember == null)
                request.SortMember = SortMember.CreatedOn;
            if (request.SortDirection == null)
                request.SortDirection = SortDirection.Asc;

            permissions = permissions.OrderBy($"{request.SortMember} {request.SortDirection}");

            return permissions;
        }


        public async Task<object> GetAllTreeAsync()
        {
            var request = new PermissionSearchRequest();
            var permissions = await QueryByRequest(request).ProjectTo<PermissionViewModel>().ToListAsync();
            return permissions.Select(model => new
            {
                model.Id,
                model.Title,
                model.ParentId,
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IList<JsTreeObject>> GetAllTreeByRoleIdAsync(Guid roleId)
        {
            var request = new PermissionSearchRequest();
            var permissions = await QueryByRequest(request).ProjectTo<PermissionViewModel>().ToListAsync();
            var rolePermissions = await GetPermissionsByRoleIdAsync(roleId);
            return permissions.Select(model => new JsTreeObject
            {
                Id = model.Id,
                ParentId = model.ParentId,
                Title = model.Title,
                IsSelect = rolePermissions.Select(m => m.Id).ToList().Contains(model.Id.Value)
            }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public async Task<IList<Guid>> GetIdsByNamesAsync(IList<string> names)
        {
            return await _permissionRepository.AsNoTracking()
                .Where(model => names.Contains(model.Name))
                .Select(model => model.Id)
                .ToListAsync();
        }

        #endregion Public Methods
    }
}