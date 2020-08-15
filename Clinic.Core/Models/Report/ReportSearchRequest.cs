using Clinic.Core.Models.Common;
using System;

namespace Clinic.Core.Models.Report
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportSearchRequest : BaseSearchRequest
    {
        #region Public Properties

        public Guid? CreatedById { get; set; }

        #endregion Public Properties
    }
}