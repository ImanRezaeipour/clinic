using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Report
{
    public class ReportViewModel : BaseViewModel
    {
        #region Public Properties

        public Guid? Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        #endregion Public Properties
    }
}