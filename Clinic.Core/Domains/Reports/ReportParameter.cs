using Clinic.Core.Domains.Common;
using System.Collections.Generic;

namespace Clinic.Core.Domains.Reports
{
    public class ReportParameter : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string PartialName { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}