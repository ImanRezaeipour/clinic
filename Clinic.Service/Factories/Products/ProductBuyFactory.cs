using System;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Models.ProductBuy;
using Clinic.Service.Services.Products;

namespace Clinic.Service.Factories.Products
{
    public class ProductBuyFactory : IProductBuyFactory
    {

        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IProductBuyService _productBuyService;
        private readonly IProductSellerService _productSellerService;

        #endregion Private Fields

        #region Public Constructors

        public ProductBuyFactory(IProductBuyService productBuyService, IProductSellerService productSellerService, IMapper mapper)
        {
            _productBuyService = productBuyService;
            _productSellerService = productSellerService;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductBuyEditViewModel> PrepareEditViewModelAsync(Guid productBuyId)
        {
            var productBuy = await _productBuyService.FindByIdAsync(productBuyId);
            var viewModel = _mapper.Map<ProductBuyEditViewModel>(productBuy);
            viewModel.SellersList = await _productSellerService.GetProductSellerAsSelectItemList();

            return viewModel;
        }

        #endregion Public Methods

    }
}