using System;
using System.Threading.Tasks;
using Clinic.Core.Models.ProductBuy;

namespace Clinic.Service.Factories.Products
{
    public interface IProductBuyFactory
    {
        Task<ProductBuyEditViewModel> PrepareEditViewModelAsync(Guid productBuyId);
    }
}