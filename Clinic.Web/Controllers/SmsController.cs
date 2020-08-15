using System.Web.Mvc;
using Clinic.Service.Services.Messages;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class SmsController : Controller
    {
        #region Private Fields

        private readonly ISmsService _smsService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="smsService"></param>
        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        #endregion Public Constructors
    }
}