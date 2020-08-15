using System.Threading.Tasks;
using System.Web.Mvc;
using Clinic.FrameWork.Filters;
using Clinic.Service.Services.Statistics;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class StatisticController : BaseController
    {
        #region Private Fields

        private readonly IStatisticService _statisticService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="statisticService"></param>
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<ActionResult> List()
        {
            return View(MVC.Statistic.Views.List);
        }

        #endregion Public Methods
    }
}