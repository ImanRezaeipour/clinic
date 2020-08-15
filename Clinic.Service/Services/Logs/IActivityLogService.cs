using System;
using System.Threading.Tasks;
using Clinic.Core.Models.ActivityLog;

namespace Clinic.Service.Services.Logs
{
    /// <summary>
    /// </summary>
    public interface IActivityLogService
    {
        #region Public Methods

        /// <summary>
        ///ایجاد دسته
        /// </summary>
        /// <param name="viewModel"></param>
        Task CreateByViewModelAsync(ActivityLogCreateViewModel viewModel);

        /// <summary>
        /// </summary>
        /// <param name="activityLogId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid activityLogId);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task UpdateByViewModelAsync(ActivityLogEditViewModel viewModel);

        #endregion Public Methods
    }
}