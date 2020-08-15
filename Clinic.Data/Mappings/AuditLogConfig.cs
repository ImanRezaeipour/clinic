using System.Data.Entity.ModelConfiguration;
using Clinic.Core.Domains;

namespace Clinic.Data.Mappings
{
    /// <summary>
    /// </summary>
    public class AuditLogConfig : EntityTypeConfiguration<AuditLog>
    {
        /// <summary>
        /// </summary>
        public AuditLogConfig()
        {

        }
    }
}