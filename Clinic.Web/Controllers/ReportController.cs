using Clinic.Core.Models.Report;
using Clinic.Service.Services.Reports;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Clinic.FrameWork.Filters;
using Clinic.Service.Factories.Reports;
using Elmah.ContentSyndication;
using Newtonsoft.Json;

namespace Clinic.Web.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public partial class ReportController : Controller
    {
        #region Private Fields

        private readonly IReportService _reportService;
        private readonly IReportFactory _reportFactory;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportService"></param>
        public ReportController(IReportService reportService, IReportFactory reportFactory)
        {
            _reportService = reportService;
            _reportFactory = reportFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> Create()
        {
            return View(MVC.Report.Views.Create);
        }

        ///  <summary>
        /// 
        ///  </summary>
        ///  <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> Create(ReportCreateViewModel viewModel)
        {
            await _reportService.CreateByViewModelAsync(viewModel);
            return RedirectToAction(MVC.Report.List());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Delete(Guid? id)
        {
            if (id != null) await _reportService.DeleteByIdAsync(id.Value);
            return RedirectToAction(MVC.Report.List());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Detail(Guid? id,ReportParameterViewModel viewModel = null)
        {
           TempData[id.ToString()]= viewModel;
            var options = new StiMvcViewerOptions();
            return View(MVC.Report.Views.Detail, viewModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> DetailExportReport()
        {
            StiReport report = StiMvcViewer.GetReportObject();
            //StiRequestParams parameters = StiMvcViewer.GetRequestParams();

            //if (parameters.ExportFormat == StiExportFormat.Pdf)
            //{
            //    // Some actions with report when exporting to PDF
            //}

            return StiMvcViewer.ExportReportResult(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> DetailInteraction()
        {
            //StiRequestParams requestParams = StiMvcViewer.GetRequestParams();
            //if (requestParams.Action == StiAction.Variables)
            //{
            //    DataSet data = new DataSet();
            //    data.ReadXml(Server.MapPath("~/Content/Data/Demo.xml"));

            //    StiReport report = StiMvcViewer.GetReportObject();
            //    report.RegData("Demo", data);

            //    return StiMvcViewer.InteractionResult(report);
            //}

            return StiMvcViewer.InteractionResult();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> DetailPrintReport()
        {
            StiReport report = StiMvcViewer.GetReportObject();

            // Some actions with report when printing

            return StiMvcViewer.PrintReportResult(report);
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="viewModel"></param>
      /// <returns></returns>
        public virtual async Task<ActionResult> DetailReport(Guid? id, ReportParameterViewModel viewModel)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            viewModel = (ReportParameterViewModel)TempData[id.ToString()];
            var report = await _reportService.GetReportAsStiReportAsync(id.Value , viewModel);
            return StiMvcViewer.GetReportSnapshotResult(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> DetailViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = new ReportEditViewModel();
            return View(MVC.Report.Views.Edit, viewModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ReportEditViewModel viewModel)
        {
            await _reportService.EditByViewModelAsync(viewModel);
            return RedirectToAction(MVC.Report.List());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> List()
        {
            return View(MVC.Report.Views.List);
        }
        [AjaxOnly]
        [MvcAuthorize]
        public virtual async Task<JsonResult> GetListAjax()
        {
            // Result
            var request = JsonConvert.DeserializeObject<Clinic.Core.Utilities.Kendo.KendoDataSourceRequest>(Request.Url.ParseQueryString().GetKey(0));
            var list = await _reportService.ListByRequestAsync(request);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public virtual async Task<ActionResult> FromDateToDate(Guid id)
        {
            var viewModel = await _reportFactory.PrepareReportParameter(id);
            return PartialView(MVC.Report.Views._FromDateToDate,viewModel);
        }

        #endregion Public Methods
    }
}