using System;
using System.Threading.Tasks;
using Clinic.Core.Models.ProductSeller;

namespace Clinic.Service.Factories.Products
{
    public interface IProductSellerFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="productSellerId"></param>
        /// <returns></returns>
        Task<ProductSellerEditViewModel> PrepareEditViewModelAsync(Guid productSellerId);
    }
}