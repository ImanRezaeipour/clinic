using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Clinic.Core.Models.Patient;
using Clinic.FrameWork.Extensions;
using Clinic.FrameWork.Filters;
using Clinic.FrameWork.Results;
using Clinic.FrameWork.Toast;
using Clinic.Service.Factories.Persons;
using Clinic.Service.Services.Persons;
using Clinic.Service.Validations;
using MvcSiteMapProvider;
using Newtonsoft.Json;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class PatientController : BaseController
    {

        #region Private Fields

        private readonly IPatientService _patientService;
        private readonly IPatientFactory _patientFactory;
        private readonly IPatientValidator _patientValidator;

        #endregion Private Fields

        #region Public Constructors

       
        public PatientController(IPatientService patientService, IPatientFactory patientFactory, IPatientValidator patientValidator)
        {
            _patientService = patientService;
            _patientFactory = patientFactory;
            _patientValidator = patientValidator;
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
        public virtual async Task<ActionResult> Create(PatientCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            // Validation
            if (ModelState.IsValid == false)
                return View(MVC.Patient.Views.Create);
            var validattor = await _patientValidator.CreateValidationAsync(viewModel.NationalCode);
            if (validattor != null)
            {
                ModelState.AddModelError("1", "کدملی تکراری است");
                return View(MVC.Patient.Views.Create);
            }

            // Result
                await _patientService.CreateByViewModelAsync(viewModel);
          
                this.AddToastMessage("افزودن بیمار با موفقیت انجام شد", "", ToastType.Success);
                return RedirectToAction(MVC.Document.Create(viewModel.NationalCode));
        }

       

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public virtual async Task<JsonResult> DeleteAjax(Guid? id)
        {
            if (id != null)
            {
               await _patientService.DeleteByIdAsync(id.Value);
            }
            return Json(AjaxResult.Succeeded(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nationalCode"></param>
        /// <returns></returns>
        [MvcAuthorize]
      [MvcSiteMapNode(Title = "ویرایش بیمار", Key = "Panel_Patient_Edit", ParentKey = "Panel_Patient_List", PreservedRouteParameters = "id")]
      [Route("Patient/Edit/{nationalCode}")]
        public virtual async Task<ActionResult> Edit(string nationalCode)
        {
            // Check
            if (nationalCode == null)
                return View(MVC.Error.Views.BadRequest);

            // Result
            var viewModel = await _patientFactory.PrepareEditViewModelAsync(nationalCode);
            return viewModel == null ? View(MVC.Error.Views.InternalServerError) : View(MVC.Patient.Views.Edit, viewModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        [MvcAuthorize]
        public virtual async Task<JsonResult> GetListAjax()
        {
            // Result
            var kendoDataSourceRequest = JsonConvert.DeserializeObject<Clinic.Core.Utilities.Kendo.KendoDataSourceRequest>(Request.Url.ParseQueryString().GetKey(0));
            var list = await _patientService.ListByRequestAsync(kendoDataSourceRequest);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "لیست بیماران", Key = "Panel_Patient_List", ParentKey = "Panel_Home_Dashboard")]
        public virtual async Task<ActionResult> List()
        {
            //  Result
            return View(MVC.Patient.Views.List);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "افزودن بیمار", Key = "Panel_Patient_New", ParentKey = "Panel_Patient_List")]
        public virtual async Task<ActionResult> Create()
        {
            //  Result
            return View(MVC.Patient.Views.Create);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [HttpPost]
        [Route("Patient/Edit")]
        public virtual async Task<ActionResult> Edit(PatientEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            // Validation
            if (ModelState.IsValid == false)
                return View(MVC.Patient.Views.Create);

            // Result
          await _patientService.UpdateByViewModelAsync(viewModel);
             this.AddToastMessage("ویرایش بیمار با موفقیت انجام شد", "", ToastType.Success);
                return RedirectToAction(MVC.Patient.List());
            
        }

        public virtual async Task<JsonResult> IsExistNationalCodeAjax(string nationalCode)
        {
            var result = await _patientService.IsExistNationalCodeAsync(nationalCode);
            return Json(AjaxResult.Succeeded(result), JsonRequestBehavior.AllowGet);
        }

        public virtual async Task<JsonResult> GetSelectListAjax()
        {
            var presenters = await _patientService.GetAsSelectListItem();
            return Json(presenters, JsonRequestBehavior.AllowGet);
        }

        #endregion Public Methods
    }
}