using System;

namespace Clinic.Core.Models.Patient
{
    public class PatientEditViewModel
    {

        #region Public Properties

        /// <summary>
        ///
        /// </summary>
        public Guid AddressId { get; set; }
        public virtual string CertificateCode { get; set; }
        public virtual string BirthDayPlace { get; set; }


        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public string BirthDayOn { get; set; }

        /// <summary>
        /// کد پرونده
        /// </summary>
        public string CustomeCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Guid DoctorId { get; set; }

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

        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string NationalCode { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Guid PresentId { get; set; }

        #endregion Public Properties

    }
}