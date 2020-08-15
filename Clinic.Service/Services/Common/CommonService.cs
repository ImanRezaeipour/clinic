using System;
using System.Threading.Tasks;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Common
{

    public class CommonService : ICommonService
    {
        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="webContextManager"></param>
        public CommonService(IHttpContextManager httpContextManager)
        {
            _httpContextManager = httpContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="isCurrentUser"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Guid?> GetUserIdAsync(bool isCurrentUser, Guid? userId)
        {
            if (isCurrentUser)
                return _httpContextManager.CurrentUserId();

            return userId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int RandomNumberByCount(int min, int max)
        {
            var random = new Random().Next(min, max);
            return random;
        }

       

        #endregion Public Methods
    }
}