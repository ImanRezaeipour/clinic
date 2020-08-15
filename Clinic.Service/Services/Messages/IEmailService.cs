using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Email;

namespace Clinic.Service.Services.Messages
{
    /// <summary>
    /// </summary>
    public interface IEmailService
    {
        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(EmailCreateViewModel viewModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid emailId);

       

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task UpdateByViewModelAsync(EmailEditViewModel viewModel);

        #endregion Public Methods
    }
}