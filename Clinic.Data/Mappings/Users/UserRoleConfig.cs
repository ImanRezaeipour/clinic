using System.Data.Entity.ModelConfiguration;
using Clinic.Core.Domains.Users;

namespace Clinic.Data.Mappings.Users
{
    /// <summary>
    /// </summary>
    public class UserRoleConfig : EntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// </summary>
        public UserRoleConfig()
        {
            HasKey(role => new { role.UserId, role.RoleId });
        }
    }
}