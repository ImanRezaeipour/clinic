using System;
using System.Threading.Tasks;
using Clinic.Core.Models.AuditLog;

namespace Clinic.Service.Services.Logs
{
    /// <summary>
    /// </summary>
    public interface IAuditLogService
    {
        /// <summary>
        ///ایجاد دسته
        /// </summary>
        /// <param name="viewModel"></param>
        Task CreateByViewModelAsync(AuditLogCreateViewModel viewModel);

        /// <summary>
        /// </summary>
        /// <param name="auditLogId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid auditLogId);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task UpdateByViewModelAsync(AuditLogEditViewModel viewModel);
    }
}