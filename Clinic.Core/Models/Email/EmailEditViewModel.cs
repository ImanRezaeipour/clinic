﻿using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Email
{
    /// <summary>
    /// </summary>
    public class EmailEditViewModel : BaseViewModel
    {
        /// <summary>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     نمایش یا عدم نمایش نفد و بررسی کمپانی
        /// </summary>
        public bool Active { get; set; }

      
        /// <summary>
        /// توضیح برای عدم تائید
        /// </summary>
        public string RejectDescription { get; set; }

        public string CategoryTitle { get; set; }

        public Guid CategoryId { get; set; }

        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public string AvatarFileName { get; set; }

        public string TitleCompany { get; set; }

        public string CompanyLogoFileName { get; set; }
    }
}