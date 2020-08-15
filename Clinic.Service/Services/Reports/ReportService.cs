using AutoMapper;
using Clinic.Core.Domains.Reports;
using Clinic.Core.Models.Report;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Clinic.Core.Configuration;
using Clinic.Core.Extensions;
using Clinic.Core.Models.Expertise;
using Clinic.Core.Utilities.Kendo;
using Stimulsoft.Report.Dictionary;

namespace Clinic.Service.Services.Reports
{
    /// <summary>
    ///
    /// </summary>
    public class ReportService : IReportService
    {
        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IConfigurationManager _configurationManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<Report> _reportRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="unitOfWork"></param>
        ///  <param name="mapper"></param>
        /// <param name="httpContextManager"></param>
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextManager httpContextManager, IConfigurationManager configurationManager)
        {
            _unitOfWork = unitOfWork;
            _reportRepository = unitOfWork.Set<Report>();
            _mapper = mapper;
            _httpContextManager = httpContextManager;
            _configurationManager = configurationManager;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(ReportCreateViewModel viewModel)
        {
            var report = _mapper.Map<Report>(viewModel);

            var buffer = new byte[viewModel.ContentFile.InputStream.Length];
            viewModel.ContentFile.InputStream.Read(buffer, 0, buffer.Length);
            report.Content = Encoding.UTF8.GetString(buffer);
             
            _reportRepository.Add(report);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid reportId)
        {
            var report = await FindByIdAsync(reportId);
            _reportRepository.Remove(report);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task EditByViewModelAsync(ReportEditViewModel viewModel)
        {
            var report = await FindByIdAsync(viewModel.Id);
            _mapper.Map(viewModel, report);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task<Report> FindByIdAsync(Guid reportId)
        {
            return await _reportRepository.SingleOrDefaultAsync(model => model.Id == reportId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task<StiReport> GetReportAsStiReportAsync(Guid reportId, ReportParameterViewModel viewModel)
        {
            var report = await FindByIdAsync(reportId);
            var stiReport = new StiReport();
            var encoding = Encoding.UTF8;
            var docAsBytes = encoding.GetBytes(report.Content);
            stiReport.Load(docAsBytes);
            foreach (StiDatabase db in stiReport.Dictionary.Databases)
            {
                ((StiSqlDatabase) db).ConnectionString = _configurationManager.ConnectionString;
            }
            foreach (var item in viewModel.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (item.GetValue(viewModel) == null)
                    continue;

                if (item.FieldType == typeof(DateTime?))
                {
                    var value = ((DateTime) item.GetValue(viewModel)).ToString("yyyy-MM-dd");
                    stiReport.Dictionary.Variables.Add(item.Name.GetNameViewModel(), value);
                }
                else
                    stiReport.Dictionary.Variables.Add(item.Name.GetNameViewModel(), item.GetValue(viewModel));
            }
            return stiReport;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IQueryable<Report> QueryByRequest(ReportSearchRequest request)
        {
            var reports = _reportRepository.AsNoTracking().AsQueryable();

            if (request.CreatedById != null)
                reports = reports.Where(model => model.CreatedById == request.CreatedById);

            //reports = reports.OrderBy($"{request.SortMember} {request.SortDirection}");

            return reports;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var result = _reportRepository.AsNoTracking().ToDataSourceResult(request);

            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<ReportViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        #endregion Public Methods
    }
}