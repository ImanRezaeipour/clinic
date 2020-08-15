using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Domains.Doctors;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Doctor;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Persons
{
    public interface IDoctorService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(DoctorCreateViewModel viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid doctorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task EditByViewModelAsync(DoctorEditViewModel viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetDoctorAsSelectItemListAsync();

        Task<Doctor> FindByIdAsync(Guid doctorId);
    }
}