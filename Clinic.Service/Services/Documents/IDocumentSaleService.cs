using System;
using System.Threading.Tasks;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Domains.Sales;
using Clinic.Core.Models.DocumentSale;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Documents
{
    public interface IDocumentSaleService
    {
        Task CreateByViewModelAsync(DocumentSaleCreateViewModel viewModel);
        Task DeleteByIdAsync(Guid documentSaleid);
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);
        Task EditByViewModelAsync(DocumentSaleEditViewModel viewModel);
        Task<Sale> FindByIdAsync(Guid documentSaleId);
    }
}