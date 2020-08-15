using System.Data.Entity.ModelConfiguration;
using Clinic.Core.Domains.Addresses;

namespace Clinic.Data.Mappings
{
    /// <summary>
    /// </summary>
    public class AddressConfig : EntityTypeConfiguration<Address>
    {
        /// <summary>
        /// </summary>
        public AddressConfig()
        {
        }
    }
}