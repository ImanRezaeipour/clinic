using Clinic.FrameWork.Results;
using Clinic.Service.Services.Expertises;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    public partial class ExpertiseController : Controller
    {
        #region Private Fields

        private readonly IExpertiseService _expertiseService;

        #endregion Private Fields

        #region Public Constructors

        public ExpertiseController(IExpertiseService expertiseService)
        {
            _expertiseService = expertiseService;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<JsonResult> GetSelectListAjax()
        {
            var result = await _expertiseService.GetExpertiseSelectListItemAsync();
            return Json(AjaxResult.Succeeded(result), JsonRequestBehavior.AllowGet);
        }

        #endregion Public Methods
    }
}