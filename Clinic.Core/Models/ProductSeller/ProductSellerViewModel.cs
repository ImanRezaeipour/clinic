using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.ProductSeller
{
    public class ProductSellerViewModel : BaseViewModel
    {
        #region Public Properties

       
        public Guid Id { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// نام شرکت
        /// </summary>
        public string CompanyTitle { get; set; }

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



        #endregion Public Properties
    }
}