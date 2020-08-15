using Clinic.Core.Domains.Reports;
using System.Data.Entity.ModelConfiguration;

namespace Clinic.Data.Mappings.Reports
{
    public class ReportConfig : EntityTypeConfiguration<Report>
    {
        #region Public Constructors

        public ReportConfig()
        {
        }

        #endregion Public Constructors
    }
}