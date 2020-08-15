using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Core.Models.Report;
using Clinic.Service.Services.Persons;
using Clinic.Service.Services.Reports;

namespace Clinic.Service.Factories.Reports
{
    public class ReportFactory : IReportFactory
    {
        private readonly IReportService _reportService;
        private readonly IDoctorService _doctorService;
        private readonly IPresenterService _presenterService;

        public ReportFactory(IReportService reportService, IDoctorService doctorService, IPresenterService presenterService)
        {
            _reportService = reportService;
            _doctorService = doctorService;
            _presenterService = presenterService;
        }

        public async Task<ReportParameterViewModel> PrepareReportParameter(Guid id)
        {
            var viewModel = new ReportParameterViewModel
            {
                DoctorList = await _doctorService.GetDoctorAsSelectItemListAsync(),
                PresenterList = await _presenterService.GetPresenterAsSelectListItem(),
                Id = id
            };
            return viewModel;
        }
    }
}
