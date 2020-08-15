using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.ProductSeller;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Products
{
    public interface IProductSellerService
    {
        Task CreateByViewModelAsync(ProductSellerCreateViewModel viewModel);
        Task EditByViewModelAsync(ProductSellerEditViewModel viewModel);
        Task DeleteByIdAsync(Guid productSellerId);
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);
        Task<IList<SelectListItem>> GetProductSellerAsSelectItemList();
        Task<ProductSeller> FindByIdAsync(Guid productSellerId);
    }
}