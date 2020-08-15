using System.Web.Mvc;
using Clinic.Service.Services.Logs;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class AuditLogController : Controller
    {
        #region Private Fields

        private readonly IAuditLogService _auditLogService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="auditLogService"></param>
        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        #endregion Public Constructors
    }
}