using AutoMapper;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.ProductBuy;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Clinic.Service.Services.Products
{
    public class ProductBuyService : IProductBuyService
    {
        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductBuy> _productBuys;
        private readonly IProductSellerService _productSellerService;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ProductBuyService(IHttpContextManager httpContextManager, IUnitOfWork unitOfWork, IMapper mapper, IProductSellerService productSellerService)
        {
            _httpContextManager = httpContextManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productSellerService = productSellerService;
            _productBuys = unitOfWork.Set<ProductBuy>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(ProductBuyCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var productBuyMap = _mapper.Map<ProductBuy>(viewModel);
            var productBuy = _productBuys.Add(productBuyMap);
            var result = await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productBuyId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid productBuyId)
        {
            // Check

            // Process
            var productBuy = await _productBuys.FirstOrDefaultAsync(model => model.Id == productBuyId);
            _productBuys.Remove(productBuy);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task EditByViewModelAsync(ProductBuyEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var productBuy = await _productBuys.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, productBuy);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        public async Task<ProductBuy> FindByIdAsync(Guid productBuyId)
        {
            return await _productBuys.SingleOrDefaultAsync(model => model.Id == productBuyId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var result = _productBuys.AsNoTracking().ToDataSourceResult(request);

            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<ProductBuyViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        #endregion Public Methods
    }
}