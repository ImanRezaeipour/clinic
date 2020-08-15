using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Report;

namespace Clinic.Service.Factories.Reports
{
    public interface IReportFactory
    {
        Task<ReportParameterViewModel> PrepareReportParameter(Guid id);
    }
}