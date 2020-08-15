using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains;
using Clinic.Core.Models.AuditLog;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Logs
{
    /// <summary>
    ///
    /// </summary>
    public class AuditLogService : IAuditLogService
    {

        #region Private Fields

        private readonly IDbSet<AuditLog> _auditLogs;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public AuditLogService(IMapper mapper, IUnitOfWork unitOfWork, IDbSet<AuditLog> auditLogStore, IHttpContextManager httpContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _auditLogs = unitOfWork.Set<AuditLog>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        public async Task CreateByViewModelAsync(AuditLogCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentException(nameof(viewModel));

            // Process
            var auditLog = _mapper.Map<AuditLog>(viewModel);
             _auditLogs.Add(auditLog);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
            
        }

        /// <summary>
        /// </summary>
        /// <param name="auditLogId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid auditLogId)
        {
            // Process
            var auditLog = await _auditLogs.FirstOrDefaultAsync(model => model.Id == auditLogId);
            _auditLogs.Remove(auditLog);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(AuditLogEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var audit = await _auditLogs.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, audit);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
            
        }

        #endregion Public Methods

    }
}