using AutoMapper;
using Clinic.Core.Models.ProductSeller;
using Clinic.Service.Services.Products;
using System;
using System.Threading.Tasks;

namespace Clinic.Service.Factories.Products
{
    public class ProductSellerFactory : IProductSellerFactory
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IProductSellerService _productSellerService;

        #endregion Private Fields

        #region Public Constructors

        public ProductSellerFactory(IProductSellerService productSellerService, IMapper mapper)
        {
            _productSellerService = productSellerService;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="productSellerId"></param>
        /// <returns></returns>
        public async Task<ProductSellerEditViewModel> PrepareEditViewModelAsync(Guid productSellerId)
        {
            var productSeller = await _productSellerService.FindByIdAsync(productSellerId);
            var productSellerMap = _mapper.Map<ProductSellerEditViewModel>(productSeller);

            return productSellerMap;
        }

        #endregion Public Methods
    }
}