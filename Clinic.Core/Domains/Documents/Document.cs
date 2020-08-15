using System;
using System.Collections.Generic;
using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Doctors;
using Clinic.Core.Domains.Patients;
using Clinic.Core.Domains.Presenters;
using Clinic.Core.Domains.Sales;

namespace Clinic.Core.Domains.Documents
{
    /// <summary>   پرونده </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class Document : BaseEntity
    {

        #region Public Properties

        /// <summary>   کد دستی برای پرونده </summary>
        /// <value> The custom code. </value>
        public virtual string CustomCode { get; set; }

        /// <summary>   توضیحات </summary>
        /// <value> The description. </value>
        public virtual string Description { get; set; }

        /// <summary>  دکتر پرونده </summary>
        /// <value> The doctor. </value>
        public virtual Doctor Doctor { get; set; }

        /// <summary>  شناسه دکتر پرونده </summary>
        /// <value> The identifier of the doctor. </value>
        public virtual Guid DoctorId { get; set; }

        /// <summary>   عکس های بیمار برای پرونده </summary>
        /// <value> The images. </value>
        public virtual ICollection<DocumentImage> Images { get; set; }

        /// <summary>   بیمار </summary>
        /// <value> The patient. </value>
        public virtual Patient Patient { get; set; }

        /// <summary>  شناسه بیمار </summary>
        /// <value> The identifier of the patient. </value>
        public virtual Guid PatientId { get; set; }
        /// <summary>  معرف بیمار </summary>
        /// <value> The presenter. </value>
        public virtual Presenter Presenter { get; set; }

        /// <summary>   شناسه معرف بیمار </summary>
        /// <value> The identifier of the presenter. </value>
        public virtual Guid PresenterId { get; set; }

        /// <summary>   علت مراجعه </summary>
        /// <value> The referral cause. </value>
        public virtual string ReferralCause { get; set; }

        /// <summary>   فاکتور های فروش برای پرونده </summary>
        /// <value> The sales. </value>
        public virtual ICollection<Sale> Sales { get; set; }

        #endregion Public Properties

    }
}