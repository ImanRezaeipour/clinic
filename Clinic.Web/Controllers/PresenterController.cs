using Clinic.Core.Models.Presenter;
using Clinic.Core.Utilities.Kendo;
using Clinic.FrameWork.Extensions;
using Clinic.FrameWork.Filters;
using Clinic.FrameWork.Results;
using Clinic.FrameWork.Toast;
using Clinic.Service.Factories.Persons;
using Clinic.Service.Services.Persons;
using MvcSiteMapProvider;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class PresenterController : BaseController
    {
        #region Private Fields

        private readonly IPresenterFactory _presenterFactory;
        private readonly IPresenterService _presenterService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="presenterService"></param>
        /// <param name="presenterFactory"></param>
        public PresenterController(IPresenterService presenterService, IPresenterFactory presenterFactory)
        {
            _presenterService = presenterService;
            _presenterFactory = presenterFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAuthorize]
        public virtual async Task<ActionResult> Create(PresenterCreateViewModel viewModel)
        {
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            if (ModelState.IsValid == false)
                return View(MVC.Presenter.Views.Create);

            await _presenterService.CreateByViewModelAsync(viewModel);
            this.AddToastMessage("افزودن بازاریاب با موفقیت انجام شد", "", ToastType.Success);
            return RedirectToAction(MVC.Presenter.List());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcSiteMapNode(Title = "ایجاد بازاریاب", Key = "Panel_Presenter_New", ParentKey = "Panel_Presenter_List")]
        [MvcAuthorize]
        public virtual async Task<ActionResult> Create()
        {
            return View(MVC.Presenter.Views.Create);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<JsonResult> DeleteAjax(Guid? id)
        {
            if (id == null)
                return Json(AjaxResult.Failed(AjaxErrorStatus.BadRequest), JsonRequestBehavior.AllowGet);

            await _presenterService.DeleteByIdAsync(id.Value);
            return Json(AjaxResult.Succeeded(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "ویرایش بازاریاب", Key = "Panel_Presenter_Edit", ParentKey = "Panel_Presenter_List", PreservedRouteParameters = "id")]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return View(MVC.Error.Views.BadRequest);

            var viewModel = await _presenterFactory.PrepareEditViewModelAsync(id.Value);
            return viewModel == null ? View(MVC.Error.Views.InternalServerError) : View(MVC.Presenter.Views.Edit, viewModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(PresenterEditViewModel viewModel)
        {
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            if (ModelState.IsValid == false)
                return View(MVC.Presenter.Views.Create);

            await _presenterService.EditByViewModelAsync(viewModel);
            this.AddToastMessage("ویرایش پزشک با موفقیت انجام شد", "", ToastType.Success);
            return RedirectToAction(MVC.Presenter.List());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        [MvcAuthorize]
        public virtual async Task<JsonResult> GetListAjax()
        {
            var request = JsonConvert.DeserializeObject<KendoDataSourceRequest>(Request.Url.ParseQueryString().GetKey(0));
            var presenters = await _presenterService.ListByRequestAsync(request);
            return Json(presenters, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<JsonResult> GetSelectListAjax()
        {
            var presenters = await _presenterService.GetPresenterAsSelectListItem();
            return Json(AjaxResult.Succeeded(presenters), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcSiteMapNode(Title = "لیست بازاریاب ها", Key = "Panel_Presenter_List", ParentKey = "Panel_Home_Dashboard")]
        [MvcAuthorize]
        public virtual async Task<ActionResult> List()
        {
            return View(MVC.Presenter.Views.List);
        }

        #endregion Public Methods
    }
}