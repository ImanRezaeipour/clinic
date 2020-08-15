using System.Data.Entity.ModelConfiguration;
using Clinic.Core.Domains.Users;

namespace Clinic.Data.Mappings.Users
{
    /// <summary>
    /// </summary>
    public class UserConfig : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// </summary>
        public UserConfig()
        {
            HasMany(user => user.Roles).WithRequired().HasForeignKey(role => role.UserId);
            HasMany(user => user.Claims).WithRequired().HasForeignKey(claim => claim.UserId);
            HasMany(user => user.Logins).WithRequired().HasForeignKey(login => login.UserId);
            HasRequired(user => user.CreatedBy).WithMany().HasForeignKey(user => user.CreatedById);
            HasOptional(user => user.ModifiedBy).WithMany().HasForeignKey(user => user.ModifiedById);

            Property(user => user.DirectPermissions).HasColumnType("xml");
            Ignore(user => user.XmlDirectPermissions);
        }
    }
}