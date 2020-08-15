using System;
using System.Threading.Tasks;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Domains.Sales;
using Clinic.Core.Models.DocumentSaleProduct;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Documents
{
    public interface IDocumentSaleProductService
    {
        Task CreateByViewModelAsync(DocumentSaleProductCreateViewModel viewModel);
        Task DeleteByIdAsync(Guid documentSaleProductid);
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);
        Task EditByViewModelAsync(DocumentSaleProductEditViewModel viewModel);
        Task<SaleProduct> FindByIdAsync(Guid documentSaleProductId);
    }
}