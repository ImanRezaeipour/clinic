using System.Data.Entity.ModelConfiguration;
using Clinic.Core.Domains.Doctors;

namespace Clinic.Data.Mappings.Doctors
{
    class DoctorConfig:EntityTypeConfiguration<Doctor>
    {
        public DoctorConfig()
        {
            
        }
    }
}
