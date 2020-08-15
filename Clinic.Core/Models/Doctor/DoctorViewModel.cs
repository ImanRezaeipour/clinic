using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Doctor
{
    public class DoctorViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// کد نظام پزشکی
        /// </summary>
        public Guid Id { get; set; }
        public string MedicalCode { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// تخصص
        /// </summary>
        public Guid ExpertiseId { get; set; }
        public string ExpertiseName { get; set; }


        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// شیفت
        /// </summary>
        public string Shift { get; set; }

        #endregion Public Properties
    }
}