using System.Data.Entity.ModelConfiguration;
using Clinic.Core.Domains.Roles;

namespace Clinic.Data.Mappings
{
    /// <summary>
    /// </summary>
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        /// <summary>
        /// </summary>
        public RoleConfig()
        {
            Property(role => role.Permissions).HasColumnType("xml");
            Ignore(r => r.XmlPermissions);
        }
    }
}