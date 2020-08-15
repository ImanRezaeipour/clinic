using System.Data.Entity;
using AutoMapper;
using Clinic.Core.Domains.Users;
using Clinic.Data.DbContexts;

namespace Clinic.Service.Services.Users
{
    /// <summary>
    /// </summary>
    public class UserLoginService :  IUserLoginService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserLogin> _userLogins;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public UserLoginService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userLogins = _unitOfWork.Set<UserLogin>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors
    }
}