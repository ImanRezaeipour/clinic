using System.Web;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Report
{
    public class ReportCreateViewModel : BaseViewModel
    {
        #region Public Properties

        public HttpPostedFileBase ContentFile { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        #endregion Public Properties
    }
}