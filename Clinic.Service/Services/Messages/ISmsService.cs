using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Sms;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Services.Messages
{
    /// <summary>
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(SmsCreateViewModel viewModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="smsId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid smsId);


        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendAsync(IdentityMessage message);

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task  UpdateByViewModelAsync(SmsEditViewModel viewModel);
    }
}