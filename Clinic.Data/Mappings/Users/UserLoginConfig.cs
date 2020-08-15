using System.Data.Entity.ModelConfiguration;
using Clinic.Core.Domains.Users;

namespace Clinic.Data.Mappings.Users
{
    /// <summary>
    /// </summary>
    public class UserLoginConfig : EntityTypeConfiguration<UserLogin>
    {
        /// <summary>   Default constructor. </summary>
        /// <remarks>   Iman, 06/04/1396. </remarks>
        public UserLoginConfig()
        {
            HasKey(login => new {login.LoginProvider, login.ProviderKey, login.UserId});
        }
    }
}