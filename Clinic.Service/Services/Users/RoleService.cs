using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Clinic.Core.Domains.Roles;
using Clinic.Core.Domains.Users;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Role;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Services.Users
{
    /// <summary>
    /// </summary>
    public class RoleService : RoleManager<Role, Guid>, IRoleService
    {

        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<Role> _roles;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="unitOfWork"></param>
        ///  <param name="mapper"></param>
        ///  <param name="applicationPermissionService"></param>
        ///  <param name="roleStore"></param>
        /// <param name="httpContextManager"></param>
        /// <param name="listManager"></param>
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IPermissionManager applicationPermissionService, IRoleStore<Role, Guid> roleStore, IHttpContextManager httpContextManager) : base(roleStore)
        {
            _applicationPermissionService = applicationPermissionService;
            _httpContextManager = httpContextManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roles = unitOfWork.Set<Role>();
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
        ///
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<bool> AnyByNameAsync(string roleName)
        {
            // Check
            if (roleName == null)
                throw new ArgumentNullException(nameof(roleName));

            // Process
            var name = await Roles.AnyAsync(model => model.Name == roleName);

            // Result
            return name;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(RoleCreateViewModel viewModel)
        {
            //  Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            //  Process
            var role = _mapper.Map<Role>(viewModel);

            var permissions = viewModel.Permissions.Split(',');
            var doc = new XDocument();
            doc.Add(new XElement("Permissions", permissions.Select(x => new XElement("Permission", x))));
            role.Permissions = doc.ToString();

            var created = _roles.Add(role);
            var result = await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModels"></param>
        public async Task CreateRangeByViewModelAsync(IEnumerable<RoleCreateViewModel> viewModels)
        {
            //  Process
            var roles = _mapper.Map<List<Role>>(viewModels);
            _unitOfWork.AddThisRange(roles);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
            
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid roleId)
        {
            // Process
            var role = await _roles.FirstOrDefaultAsync(model => model.Id == roleId);
            _roles.Remove(role);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        public async Task<RoleEditViewModel> FillForEditAsync(RoleEditViewModel viewModel)
        {
            //  Process
            var permissions = AccessPermissionManager.GetAsSelectListItems();

            //  Result
            return viewModel;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetAllAsSelectListAsync()
        {
            //  Process
            var roles = await _roles.ToListAsync();
            var selectListItems = _mapper.Map<List<SelectListItem>>(roles);

            //  Result
            return selectListItems;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Role>> GetAllAsync()
        {
            //  Process
            var roleList = await Roles.ToListAsync();

            //  Result
            return roleList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetAsSelectListItemAsync()
        {
            //  Process
            var result = await Roles.Select(record => new SelectListItem { Value = record.Id.ToString(), Text = record.Name }).ToListAsync();

            //  Result
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<RoleEditViewModel> GetForEditAsync(Guid roleId)
        {
            //  Process
            var role = await _roles.FirstOrDefaultAsync(model => model.Id == roleId);
            var viewModel = _mapper.Map<RoleEditViewModel>(role);

            await FillForEditAsync(viewModel);

            //  Result
            return viewModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PermissionViewModel>> GetPermissionsAsync()
        {
            //  Process
            var permissions = AccessPermissionManager.GetAsSelectListItems();

            //  Result
            return permissions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IList<PermissionViewModel>> GetPermissionsByRoleIdAsync(Guid roleId)
        {
            //  Process
            var permissions = AccessPermissionManager.GetAsSelectListItems();

            var role = await _roles.FirstOrDefaultAsync(model => model.Id == roleId);
            var document = XDocument.Parse(role.Permissions);
            var rolePermissions = permissions.Select(model => new PermissionViewModel
            {
                Id = model.Id,
                ParentId = model.ParentId,
                Title = model.Title,
                Audit = model.Audit,
                IsSelect = document.Descendants("Permission").Any(m => m.Value == model.Id)
            }).ToList();

            //  Result
            return rolePermissions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetPermissionsByUserIdAsync(Guid userId)
        {
            //  Process
            var userRolesQuery = await GetRolesWithPermisionsByUserIdAsync(userId);

            var roleNames = await GetRoleNameByUserIdAsync(userId);

            var result = roleNames.Union(_applicationPermissionService.GetUserPermissionsAsList(userRolesQuery.Select(a => XElement.Parse(a.Permissions)).ToList())).ToList();

            //  Result
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetRoleNameByUserIdAsync(Guid userId)
        {
            //  Process
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select new { role.Name, role.Permissions };

            var roles = await userRolesQuery.AsNoTracking().ToListAsync();
            var roleNames = roles.Select(a => a.Name).ToList();

            //  Result
            return roleNames;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string[]> GetRolesByUserIdAsync(Guid userId)
        {
            //  Process
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var result = await userRolesQuery.OrderBy(x => x.Name).Select(a => a.Name).ToListAsync();

            //  Result
            return !result.Any() ? new string[] { } : result.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<Role>> GetRolesWithPermisionsByUserIdAsync(Guid userId)
        {
            //  Process
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var roles = await userRolesQuery.AsNoTracking().ToListAsync();

            //  Result
            return roles;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<Guid>> GetUserRoleIdByUserIdAsync(Guid userId)
        {
            //  Process
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var result = await userRolesQuery.Select(a => a.Id).ToListAsync();

            //  Result
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetUserRoleNameByUserIdAsync(Guid userId)
        {
            // Process
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var result = await userRolesQuery.OrderBy(x => x.Name).Select(a => a.Name).ToListAsync();

            // Resut
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IList<UserRole>> GetUsersByRoleNameAsync(string roleName)
        {
            // Process
            var result = await Roles.Where(role => role.Name == roleName).SelectMany(role => role.Users).ToListAsync();

            // Result
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsSystemRoleByIdAsync(Guid id)
        {
            //  Process
            var result = await Roles.AnyAsync(model => model.Id == id && model.IsSystemRole == true);

            //  Result
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId">  </param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<bool> IsUserInRoleAsync(Guid userId, string roleName)
        {
            //  Process
            var userRolesQuery = from role in Roles
                                 where role.Name == roleName
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var userRole = await userRolesQuery.FirstOrDefaultAsync();

            //  Result
            return userRole != null;
        }

        /// <summary>
        ///
        /// </summary>
        public async Task SeedAsync()
        {
            var standardRoles = RolePermissionManager.SystemRolesWithPermissions;

            foreach (var role in from record in standardRoles
                                 let role = this.FindByName(record.RoleName)
                                 where role == null
                                 select new Role
                                 {
                                     Name = record.RoleName,
                                     IsSystemRole = true,
                                     XmlPermissions =
                                         _applicationPermissionService.GetPermissionsAsXml(record.Permissions.Select(a => a.Name)
                                             .ToArray())
                                 }
            )
            {
                _roles.Add(role);
            }
            await _unitOfWork.SaveAllChangesAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<RoleViewModel>> TreeRoleAsync()
        {
            // Process
            var roles = await _roles.AsNoTracking().Where(model => model.IsDelete == false).ToListAsync();
            var rolesViewModel = _mapper.Map<IList<RoleViewModel>>(roles);

            // Result
            return rolesViewModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(RoleEditViewModel viewModel)
        {
            //  Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            //  Procss
            var role = await _roles.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            var updated = _mapper.Map(viewModel, role);

            var permissions = viewModel.Permissions.Split(',');
            var document = new XDocument();
            var element = new XElement("Permissions", permissions.Select(x => new XElement("Permission", x)));
            document.Add(element);
            updated.Permissions = document.ToString();

          await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods

    }
}