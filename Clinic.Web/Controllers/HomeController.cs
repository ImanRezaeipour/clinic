using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.UI;
using Clinic.FrameWork.Filters;
using Clinic.FrameWork.Results;
using Clinic.Service.Services;
using MvcSiteMapProvider;

namespace Clinic.Web.Controllers
{
    /// <summary>
    /// </summary>
    public partial class HomeController : BaseController
    {
        #region Private Fields

        private readonly IHomeService _homeService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="rndDate"></param>
        /// <returns></returns>
      //  [NoBrowserCache]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "None")]
        public virtual CaptchaImageResult CaptchaImage(string rndDate)
        {
            return new CaptchaImageResult();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "پروفایل کاربری", Key = "Panel_Home_Dashboard", ParentKey = "")]
        [OutputCache(Duration = 600000, VaryByParam = "none")]
        public virtual ActionResult Dashboard()
        {

            //  Result
            //var viewModel=
            return View(MVC.Home.Views.Dashboard);
        }

        [OutputCache(Duration = 200000, VaryByParam = "*")]
        public virtual ActionResult Iman()
        {
            return Content(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        #endregion Public Methods
    }
}