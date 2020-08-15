using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Models.Role;
using Clinic.Service.Services.Common;
using Clinic.Service.Services.List;
using Clinic.Service.Services.Roles;

namespace Clinic.Service.Factories.Users
{

    public class RoleFactory : IRoleFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListManager _listManager;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="listManager"></param>
        /// <param name="commonService"></param>
        /// <param name="mapper"></param>
        /// <param name="roleService"></param>
        public RoleFactory(IListManager listManager, ICommonService commonService, IMapper mapper, IRoleService roleService)
        {
            _listManager = listManager;
            _commonService = commonService;
            _mapper = mapper;
            _roleService = roleService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<RoleEditViewModel> PrepareEditViewModelAsync(Guid roleId)
        {
            if (roleId == null)
                throw new ArgumentNullException(nameof(roleId));

            var role = await _roleService.FindAsync(roleId);
            var viewModel = _mapper.Map<RoleEditViewModel>(role);

            return viewModel;
        }


        public async Task<RoleListViewModel> PrepareListViewModelAsync(RoleSearchRequest request, bool isCurrentUser = false, Guid? userId = null)
        {
            request.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            request.TotalCount = await _roleService.CountByRequestAsync(request);
            var role = await _roleService.GetRolesByRequestAsync(request);
            var roleViewModel = _mapper.Map<IList<RoleViewModel>>(role);
            var viewModel = new RoleListViewModel
            {
                SearchRequest = request,
                Roles = roleViewModel,
                PageSizeList = await _listManager.GetPageSizeListAsync(),
                SortDirectionList = await _listManager.GetSortDirectionListAsync(),
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}