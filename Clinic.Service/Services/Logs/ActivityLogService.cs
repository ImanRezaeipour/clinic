using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains;
using Clinic.Core.Models.ActivityLog;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Logs
{
    /// <summary>
    /// </summary>
    public class ActivityLogService : IActivityLogService
    {

        #region Private Fields

        private readonly IDbSet<ActivityLog> _activityLogs;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="mapper"></param>
        ///  <param name="unitOfWork"></param>
        /// <param name="httpContextManager"></param>
        /// <param name="listManager"></param>
        public ActivityLogService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _activityLogs = unitOfWork.Set<ActivityLog>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(ActivityLogCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentException(nameof(viewModel));

            // Process
            var activityLog = _mapper.Map<ActivityLog>(viewModel);
             _activityLogs.Add(activityLog);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// </summary>
        /// <param name="activityLogId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid activityLogId)
        {
            // Process
            var activityLog = await _activityLogs.FirstOrDefaultAsync(model => model.Id == activityLogId);
            _activityLogs.Remove(activityLog);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(ActivityLogEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var originalActivityLog = _activityLogs.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, originalActivityLog);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods

    }
}