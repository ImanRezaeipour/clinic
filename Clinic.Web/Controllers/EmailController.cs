using System.Web.Mvc;
using Clinic.Service.Services.Messages;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class EmailController : Controller
    {
        #region Private Fields

        private readonly IEmailService _emailService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="emailService"></param>
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        #endregion Public Constructors
    }
}