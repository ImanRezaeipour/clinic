using System;

namespace Clinic.Core.Models.Patient
{
    public class PatientViewModel
    {
        #region Public Properties

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public string BirthDayOn { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }

        public string BirthDayPlace { get; set; }

        public string CertificateCode { get; set; }

        public string NationalCode { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        public string PhoneNumber { get; set; }

        public string CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsActive { get; set; }
        public bool IsActivqe { get; set; }

        #endregion Public Properties
    }
}