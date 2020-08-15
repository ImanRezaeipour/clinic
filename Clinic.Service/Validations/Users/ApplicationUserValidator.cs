﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Clinic.Core.Domains.Users;
using Clinic.Service.Services;
using Clinic.Service.Services.Users;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Validations.Users
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class ApplicationUserValidator<TUser, TKey> : IIdentityValidator<User> where TUser : class, IUser<Guid> where TKey : IEquatable<Guid>
    {
        #region Public Constructors

        /// <summary>
        /// </summary>
        /// <param name="service"></param>
        public ApplicationUserValidator(UserService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            AllowOnlyAlphanumericUserNames = true;
            Service = service;
        }

        #endregion Public Constructors



        #region Public Properties

        public bool AllowOnlyAlphanumericUserNames { get; set; }

        public bool RequireUniqueEmail { get; set; }

        #endregion Public Properties



        #region Private Properties

        private UserService Service { get; }

        #endregion Private Properties



        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> ValidateAsync(User item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var errors = new List<string>();
            await ValidateUserName(item, errors);
            if (RequireUniqueEmail)
                await ValidateEmailAsync(item, errors);
            return errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

        #endregion Public Methods



        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="user">  </param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private async Task ValidateEmailAsync(User user, ICollection<string> errors)
        {
            var email = await Service.GetEmailStore().GetEmailAsync(user).WithCurrentCulture();
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("وارد کردن ایمیل ضروریست");
            }
            else
            {
                try
                {
                    var m = new MailAddress(email);
                }
                catch (FormatException)
                {
                    errors.Add("ایمیل را به شکل صحیح وارد کنید");
                    return;
                }
                var owner = await Service.FindByEmailAsync(email);
                if (owner != null && !EqualityComparer<Guid>.Default.Equals(owner.Id, user.Id))
                    errors.Add("این ایمیل قبلا ثبت شده است");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="user">  </param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private async Task ValidateUserName(User user, ICollection<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
                errors.Add("نام کاربری نباید خالی باشد");
            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, "^[A-Za-z0-9@_\\.]+$"))
            {
                errors.Add("برای نام کاربری فقط از کاراکتر های مجاز استفاده کنید ");
            }
            else
            {
                var owner = await Service.FindByNameAsync(user.UserName);
                if (owner != null && !EqualityComparer<Guid>.Default.Equals(owner.Id, user.Id))
                    errors.Add("این نام کاربری قبلا ثبت شده است");
            }
        }

        #endregion Private Methods
    }
}