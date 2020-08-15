using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Smses;
using Clinic.Core.Models.Sms;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Services.Messages
{
    /// <summary>
    /// </summary>
    public class SmsService :  IIdentityMessageService, ISmsService
    {

        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<Sms> _smses;
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
        public SmsService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _smses = unitOfWork.Set<Sms>();
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(SmsCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var sms = _mapper.Map<Sms>(viewModel);
             _smses.Add(sms);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="smsId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid smsId)
        {
            // Process
            var sms = await _smses.FirstOrDefaultAsync(model => model.Id == smsId);
             _smses.Remove(sms);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(SmsEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var sms = await _smses.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, sms);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods

    }
}