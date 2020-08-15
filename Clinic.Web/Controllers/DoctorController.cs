using Clinic.Core.Models.Doctor;
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
    public partial class DoctorController : BaseController
    {
        #region Private Fields

        private readonly IDoctorFactory _doctorFactory;
        private readonly IDoctorService _doctorService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctorService"></param>
        /// <param name="doctorFactory"></param>
        public DoctorController(IDoctorService doctorService, IDoctorFactory doctorFactory)
        {
            _doctorService = doctorService;
            _doctorFactory = doctorFactory;
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
        public virtual async Task<ActionResult> Create(DoctorCreateViewModel viewModel)
        {
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            if (ModelState.IsValid == false)
                return RedirectToAction(MVC.Doctor.Create());

            await _doctorService.CreateByViewModelAsync(viewModel);
            this.AddToastMessage("افزودن پزشک با موفقیت انجام شد", "", ToastType.Success);
            return RedirectToAction(MVC.Doctor.List());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "افزودن پزشک", Key = "Panel_Doctor_New", ParentKey = "Panel_Doctor_List")]
        public virtual async Task<ActionResult> Create()
        {
            return View(MVC.Doctor.Views.Create);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        [MvcAuthorize]
        public virtual async Task<JsonResult> DeleteAjax(Guid? id)
        {
            if (id == null)
                return Json(AjaxResult.Failed(AjaxErrorStatus.BadRequest), JsonRequestBehavior.AllowGet);

            await _doctorService.DeleteByIdAsync(id.Value);
            return Json(AjaxResult.Succeeded(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "ویرایش پزشک", Key = "Panel_Doctor_Edit", ParentKey = "Panel_Doctor_List", PreservedRouteParameters = "id")]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return View(MVC.Error.Views.BadRequest);

            var viewModel = await _doctorFactory.PrepareEditViewModelAsync(id.Value);
            return viewModel == null ? View(MVC.Error.Views.InternalServerError) : View(MVC.Doctor.Views.Edit, viewModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(DoctorEditViewModel viewModel)
        {
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            if (ModelState.IsValid == false)
                return View(MVC.Doctor.Views.Create);

            await _doctorService.EditByViewModelAsync(viewModel);
            this.AddToastMessage("ویرایش پزشک با موفقیت انجام شد", "", ToastType.Success);
            return RedirectToAction(MVC.Doctor.List());
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
            var doctors = await _doctorService.ListByRequestAsync(request);
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        public virtual async Task<JsonResult> GetSelectListAjax()
        {
            var doctors = await _doctorService.GetDoctorAsSelectItemListAsync();
            return Json(AjaxResult.Succeeded(doctors), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "لیست پزشک ها", Key = "Panel_Doctor_List", ParentKey = "Panel_Home_Dashboard")]
        public virtual async Task<ActionResult> List()
        {
            return View(MVC.Doctor.Views.List);
        }

        #endregion Public Methods
    }
}