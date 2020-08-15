using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Configuration;
using Clinic.Core.Domains.Emails;
using Clinic.Core.Models.Email;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using Microsoft.AspNet.Identity;

namespace Clinic.Service.Services.Messages
{
    /// <summary>
    /// </summary>
    public class EmailService : IIdentityMessageService, IEmailService
    {
        #region Private Fields

        private readonly IConfigurationManager _configurationManager;
        private readonly IDbSet<Email> _emails;
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
        public EmailService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager, IConfigurationManager configurationManager)
        {
            _emails = unitOfWork.Set<Email>();
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _configurationManager = configurationManager;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(EmailCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var social = _mapper.Map<Email>(viewModel);
            _emails.Add(social);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid emailId)
        {
            // Process
            var email = await _emails.FirstOrDefaultAsync(model => model.Id == emailId);
             _emails.Remove(email);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// </summary>
        ///// <param name = "message" ></ param >
        ///// < returns ></ returns >
        //public Task SendAsync(IdentityMessage message)
        //{
        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(_configurationManager.Address),
        //        To = { new MailAddress(message.Destination) },
        //        Subject = message.Subject,
        //        Body = message.Body,
        //        IsBodyHtml = true
        //    };
        //    var smtpClient = new SmtpClient(_configurationManager.Host, Convert.ToInt32(_configurationManager.Port))
        //    {
        //        Credentials = new NetworkCredential(_configurationManager.User, _configurationManager.Password),
        //        EnableSsl = true
        //    };
        //    return smtpClient.SendMailAsync(mailMessage);
        //}

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(EmailEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var email = await _emails.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
           _mapper.Map(viewModel, email);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods
    }
}