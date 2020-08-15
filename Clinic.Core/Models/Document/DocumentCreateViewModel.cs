using Clinic.Core.Models.Common;
using Clinic.Core.Models.DocumentImage;
using System;
using System.Collections.Generic;
using Clinic.Core.Models.DocumentSale;

namespace Clinic.Core.Models.Document
{
    public class DocumentCreateViewModel : BaseViewModel
    {
        #region Public Properties

        public string CustomCode { get; set; }

        public string Description { get; set; }

        public Guid DoctorId { get; set; }

        public IEnumerable<DocumentImageCreateViewModel> Images { get; set; }

        public Guid PatientId { get; set; }

        public Guid PresenterId { get; set; }

        public string ReferralCause { get; set; }

        public IEnumerable<DocumentSaleCreateViewModel> Sales { get; set; }
        public string SalesJson { get; set; }

        #endregion Public Properties
    }
}