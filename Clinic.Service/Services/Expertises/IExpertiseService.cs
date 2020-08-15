using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Domains.Doctors;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Expertise;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Expertises
{
    public interface IExpertiseService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expertiseId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid expertiseId);

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
        Task EditByViewModelAsync(ExpertiseEditViewModel viewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetExpertiseSelectListItemAsync();

        Task<Expertise> FindByIdAsync(Guid expertiseId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(ExpertiseCreateViewModel viewModel);

        Task CreateByModelAsync(Expertise expertise);
    }
}