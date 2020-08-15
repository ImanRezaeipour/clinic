using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Product;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Products
{
    public interface IProductService
    {
        Task CreateByViewModelAsync(ProductCreateViewModel viewModel);
        Task DeleteByIdAsync(Guid productId);
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);
        Task EditByViewModelAsync(ProductEditViewModel viewModel);
        Task<IList<ProductViewModel>> TreeAsync();
        Task<Product> FindByIdAsync(Guid productId);
        Task<IList<SelectListItem>> GetAsSelectItemListAsync();
        Task<decimal?> GetPriceByIdAsync(Guid productId);
    }
}