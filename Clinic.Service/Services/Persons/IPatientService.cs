using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Domains.Patients;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Patient;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Persons
{
    public interface IPatientService
    {
        Task CreateByViewModelAsync(PatientCreateViewModel viewModel);
        Task DeleteByIdAsync(Guid patientId);
        Task UpdateByViewModelAsync(PatientEditViewModel viewModel);

        /// <summary>   List by request asynchronous. </summary>
        /// <remarks>   Iman, 08/04/1396. </remarks>
        /// <returns>   The asynchronous result that yields an Arezoo.Utility.DynamicFiltering.DataSourceResult. </returns>
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);

        Task<Patient> FindByIdAsync(Guid patientId);
        Task<bool> IsExistNationalCodeAsync(string nationalCode);
        Task<Patient> FindByNationalcodeAsync(string nationalCode);
        Task<IList<SelectListItem>> GetAsSelectListItem();
    }
}