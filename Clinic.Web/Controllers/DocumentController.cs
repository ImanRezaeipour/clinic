using Clinic.Core.Models.Document;
using Clinic.Core.Utilities.Kendo;
using Clinic.FrameWork.Extensions;
using Clinic.FrameWork.Results;
using Clinic.FrameWork.Toast;
using Clinic.Service.Factories.Documents;
using Clinic.Service.Services.Documents;
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
    public partial class DocumentController : Controller
    {
        #region Private Fields

        private readonly IDocumentFactory _documentFactory;
        private readonly IDocumentService _documentService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentService"></param>
        /// <param name="documentFactory"></param>
        public DocumentController(IDocumentService documentService, IDocumentFactory documentFactory)
        {
            _documentService = documentService;
            _documentFactory = documentFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> Create(DocumentCreateViewModel viewModel)
        {
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            if (ModelState.IsValid == false)
                return View(MVC.Document.Views.Create, viewModel);

            await _documentService.CreateByViewModelAsync(viewModel);
            this.AddToastMessage("افزودن پرونده با موفقیت انجام شد", "", ToastType.Success);
            return RedirectToAction(MVC.Document.List());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nationalCode"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Create(string nationalCode)
        {
            return View(MVC.Document.Views.Create);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> Edit(DocumentEditViewModel viewModel)
        {
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            if (ModelState.IsValid == false)
                return View(MVC.Document.Views.Edit, viewModel);

            await _documentService.EditByViewModelAsync(viewModel);
            this.AddToastMessage("Success", "", ToastType.Success);
            return RedirectToAction(MVC.Document.List());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return View(MVC.Error.Views.BadRequest);

            var viewModel = _documentFactory.PrepareEditViewModelAsync(id.Value);
            return View(MVC.Document.Views.Edit, viewModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<JsonResult> GetListAjax()
        {
            var request = JsonConvert.DeserializeObject<KendoDataSourceRequest>(Request.Url.ParseQueryString().GetKey(0));
            var documents = await _documentService.ListByRequestAsync(request);
            return Json(documents, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> List()
        {
            return View(MVC.Document.Views.List);
        }

        public virtual async Task<ActionResult> FactorProduct()
        {
            return View(MVC.Document.Views._FactorProduct);
        }

        public virtual async Task<ActionResult> Factor()
        {
            return View(MVC.Document.Views._Factor);
        }

        #endregion Public Methods
    }
}