using System;
using System.Threading.Tasks;
using Clinic.Core.Domains.Users;

namespace Clinic.Service.Services.Users
{
    public interface IUserRoleService
    {

        /// <summary>
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        Task<UserRole> FindAsync(Guid userRoleId);

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserRole> FindByUserIdAsync(Guid userId);
    }
}