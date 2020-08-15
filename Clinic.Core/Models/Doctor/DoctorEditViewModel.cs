using System;
using System.Collections.Generic;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Doctor
{
    public class DoctorEditViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// کد نظام پزشکی
        /// </summary>
        public string MedicalCode { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// تخصص
        /// </summary>
        public Guid ExpertiseId { get; set; }

        public IEnumerable<SelectListItem> ExpertiseList { get; set; }


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
        /// تلفن ثابت
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// شیفت
        /// </summary>
        public string Shift { get; set; }

        #endregion Public Properties
    }
}