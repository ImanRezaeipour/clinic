using System;
using System.Data.Entity.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Core.Domains.Users;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Services.Users.Factories
{
    /// <summary>
    /// </summary>
    public class ApplicationClaimsIdentityFactory : IClaimsIdentityFactory<User, Guid>, IApplicationClaimsIdentityFactory
    {
        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationClaimsIdentityFactory()
        {
            RoleClaimType = ClaimsIdentity.DefaultRoleClaimType;
            UserIdClaimType = ClaimTypes.NameIdentifier;
            UserNameClaimType = ClaimsIdentity.DefaultNameClaimType;
            SecurityStampClaimType = Microsoft.AspNet.Identity.Constants.DefaultSecurityStampClaimType;
        }

        #endregion Public Constructors



        #region Public Properties

        /// <summary>
        /// Claim type used for role claims
        /// </summary>
        public string RoleClaimType { get; set; }

        /// <summary>
        /// Claim type used for the user security stamp
        /// </summary>
        public string SecurityStampClaimType { get; set; }

        /// <summary>
        /// Claim type used for the user id
        /// </summary>
        public string UserIdClaimType { get; set; }

        /// <summary>
        /// Claim type used for the user name
        /// </summary>
        public string UserNameClaimType { get; set; }

        #endregion Public Properties



        #region Public Methods

        /// <summary>
        /// Create a ClaimsIdentity from a user
        /// </summary>
        /// <param name="manager">           </param>
        /// <param name="user">              </param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public virtual async Task<ClaimsIdentity> CreateAsync(UserManager<User, Guid> manager, User user, string authenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var id = new ClaimsIdentity(authenticationType, UserNameClaimType, RoleClaimType);
            id.AddClaim(new Claim(UserIdClaimType, user.Id.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(UserNameClaimType, user.UserName, ClaimValueTypes.String));
            id.AddClaim(new Claim(ApplicationClaimsIdentityConst.IdentityProviderClaimType, ApplicationClaimsIdentityConst.DefaultIdentityProviderClaimValue, ClaimValueTypes.String));
            if (manager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim(SecurityStampClaimType,
                    await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture()));
            }
            if (manager.SupportsUserRole)
            {
                var roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture();
                foreach (var roleName in roles)
                {
                    id.AddClaim(new Claim(RoleClaimType, roleName, ClaimValueTypes.String));
                }
            }
            if (manager.SupportsUserClaim)
            {
                id.AddClaims(await manager.GetClaimsAsync(user.Id).WithCurrentCulture());
            }
            return id;
        }

        #endregion Public Methods
    }
}