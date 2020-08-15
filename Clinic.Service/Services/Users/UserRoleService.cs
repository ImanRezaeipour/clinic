using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Users;
using Clinic.Data.DbContexts;

namespace Clinic.Service.Services.Users
{
    /// <summary>
    /// </summary>
    public class UserRoleService :  IUserRoleService
    {

        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserRole> _userRoles;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// </summary>
        /// <param name="mapper">    </param>
        /// <param name="unitOfWork"></param>
        public UserRoleService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRoles = unitOfWork.Set<UserRole>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public async Task<UserRole> FindAsync(Guid userRoleId)
        {
            // Process
            var userRole = await _userRoles.AsNoTracking().FirstOrDefaultAsync(model => model.RoleId == userRoleId);

            // Result
            return userRole;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserRole> FindByUserIdAsync(Guid userId)
        {
            // Process
            var userRole = await _userRoles.AsNoTracking().FirstOrDefaultAsync(model => model.UserId == userId);

            // Result
            return userRole;
        }

        #endregion Public Methods

    }
}