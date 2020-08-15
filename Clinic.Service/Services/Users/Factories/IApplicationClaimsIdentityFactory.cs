using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Core.Domains.Users;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Services.Users.Factories
{
    public interface IApplicationClaimsIdentityFactory
    {
        /// <summary>
        /// Claim type used for role claims 
        /// </summary>
        string RoleClaimType { get; set; }

        /// <summary>
        /// Claim type used for the user security stamp 
        /// </summary>
        string SecurityStampClaimType { get; set; }

        /// <summary>
        /// Claim type used for the user id 
        /// </summary>
        string UserIdClaimType { get; set; }

        /// <summary>
        /// Claim type used for the user name 
        /// </summary>
        string UserNameClaimType { get; set; }

        /// <summary>
        /// Create a ClaimsIdentity from a user 
        /// </summary>
        /// <param name="manager">           </param>
        /// <param name="user">              </param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        Task<ClaimsIdentity> CreateAsync(UserManager<User, Guid> manager, User user, string authenticationType);
    }
}