using System.Data.Entity;
using AutoMapper;
using Clinic.Core.Domains.Users;
using Clinic.Data.DbContexts;

namespace Clinic.Service.Services.Users
{
    /// <summary>
    /// </summary>
    public class UserClaimService :  IUserClaimService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserClaim> _userClaims;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public UserClaimService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userClaims = _unitOfWork.Set<UserClaim>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors
    }
}