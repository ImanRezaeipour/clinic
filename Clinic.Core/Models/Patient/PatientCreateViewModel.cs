using System;
using System.ComponentModel.DataAnnotations;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Patient
{
    public class PatientCreateViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDayOn { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        public virtual string CertificateCode { get; set; }
        public virtual string BirthDayPlace { get; set; }


        /// <summary>
        ///
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Required]

        public string LastName { get; set; }

        /// <summary>
        /// تلفن همراه
        /// </summary>
        //[StringLength(11, MinimumLength = 11)]
        //[RegularExpression("^[۰-۹0-9_]*$")]
        public string MobileNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        //[StringLength(10, MinimumLength = 10)]
        //[RegularExpression("^[۰-۹0-9_]*$")]
        public string NationalCode { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        //[StringLength(11, MinimumLength = 11)]
        //[RegularExpression("^[۰-۹0-9_]*$")]
        public string PhoneNumber { get; set; }

        #endregion Public Properties
    }
}