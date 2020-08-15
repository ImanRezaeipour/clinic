using Clinic.Service.Services.Logs;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class ActivityLogController : BaseController
    {
        #region Private Fields

        private readonly IActivityLogService _activityLogService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="activityLogService"></param>
        public ActivityLogController(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        #endregion Public Constructors
    }
}