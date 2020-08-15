using AutoMapper;
using Clinic.Core.Models.Product;
using Clinic.Service.Services.Products;
using System;
using System.Threading.Tasks;

namespace Clinic.Service.Factories.Products
{
    public class ProductFactory : IProductFactory
    {

        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        #endregion Private Fields

        #region Public Constructors

        public ProductFactory(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ProductEditViewModel> PrepareEditViewModelAsync(Guid productId)
        {
            // Process
            var product = await _productService.FindByIdAsync(productId);
            var viewModel = _mapper.Map<ProductEditViewModel>(product);

            // Result
            return viewModel;
        }

        #endregion Public Methods

    }
}