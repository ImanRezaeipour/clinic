using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Document
{
   public class DocumentViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public  string CustomCode { get; set; }

       public  string Description { get; set; }

        public  Guid DoctorId { get; set; }

       // public  ICollection<DocumentImage> Images { get; set; }

        public  Guid PatientId { get; set; }
       

        public  Guid PresenterId { get; set; }

        public  string ReferralCause { get; set; }

      
       // public  ICollection<DocumentSale> Sales { get; set; }
    }
}
