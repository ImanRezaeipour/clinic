using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Domains.Presenters;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Presenter;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Persons
{
    public interface IPresenterService
    {
        Task CreateByViewModelAsync(PresenterCreateViewModel viewModel);
        Task EditByViewModelAsync(PresenterEditViewModel viewModel);
        Task DeleteByIdAsync(Guid presentId);
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);
        Task<IList<SelectListItem>> GetPresenterAsSelectListItem();
        Task<Presenter> FindByIdAsync(Guid presenterId);
    }
}