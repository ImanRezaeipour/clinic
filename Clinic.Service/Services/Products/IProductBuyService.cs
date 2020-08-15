using System;
using System.Threading.Tasks;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.ProductBuy;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Products
{
    public interface IProductBuyService
    {
        Task CreateByViewModelAsync(ProductBuyCreateViewModel viewModel);
        Task DeleteByIdAsync(Guid productBuyId);
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);
        Task EditByViewModelAsync(ProductBuyEditViewModel viewModel);
        Task<ProductBuy> FindByIdAsync(Guid productBuyId);
    }
}