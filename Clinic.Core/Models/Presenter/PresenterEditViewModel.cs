using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Presenter
{
    public class PresenterEditViewModel : BaseViewModel
    {
        #region Public Properties

      
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

     
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
       

        #endregion Public Properties
    }
}