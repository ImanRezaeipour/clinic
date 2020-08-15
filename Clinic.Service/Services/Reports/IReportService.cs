using System;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Core.Domains.Reports;
using Clinic.Core.Models.Report;
using Clinic.Core.Utilities.Kendo;
using Stimulsoft.Report;

namespace Clinic.Service.Services.Reports
{
    public interface IReportService
    {
        Task<StiReport> GetReportAsStiReportAsync(Guid reportId, ReportParameterViewModel viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        Task<Report> FindByIdAsync(Guid reportId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IQueryable<Report> QueryByRequest(ReportSearchRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(ReportCreateViewModel viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task EditByViewModelAsync(ReportEditViewModel viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid reportId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);
    }
}