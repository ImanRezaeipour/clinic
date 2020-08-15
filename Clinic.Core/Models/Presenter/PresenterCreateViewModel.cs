using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Presenter
{
   public class PresenterCreateViewModel : BaseViewModel
    {
        

        /// <summary>
        /// نام
        /// </summary>
        public  string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public  string LastName { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        public  string PhoneNumber { get; set; }

        /// <summary>
        /// تلفن همراه
        /// </summary>
        public  string MobileNumber { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public  string Description { get; set; }
      

    }
}
