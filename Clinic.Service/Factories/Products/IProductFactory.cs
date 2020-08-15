using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Product;

namespace Clinic.Service.Factories.Products
{
    public interface IProductFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<ProductEditViewModel> PrepareEditViewModelAsync(Guid productId);
    }
}