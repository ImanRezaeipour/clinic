using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Report
{
    public class ReportParameterViewModel : BaseViewModel
    {
        public  Guid? Id { get; set; }

       public IEnumerable<SelectListItem> DoctorList { get; set; }
       public Guid? DoctorId { get; set; }
       public IEnumerable<SelectListItem> PresenterList { get; set; }
       public Guid? PresenterId { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}