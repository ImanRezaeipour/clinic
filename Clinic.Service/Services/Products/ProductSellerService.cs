using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.ProductSeller;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Products
{
    public class ProductSellerService :  IProductSellerService
    {

        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductSeller> _productSellers;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ProductSellerService(IMapper mapper, IHttpContextManager httpContextManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _httpContextManager = httpContextManager;
            _unitOfWork = unitOfWork;
            _productSellers = unitOfWork.Set<ProductSeller>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(ProductSellerCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var productSeller = _mapper.Map<ProductSeller>(viewModel);
             _productSellers.Add(productSeller);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productSellerId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid productSellerId)
        {
            var productSeller = await FindByIdAsync(productSellerId);
            _productSellers.Remove(productSeller);
           await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
      ;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productSellerId"></param>
        /// <returns></returns>
        public async Task<ProductSeller> FindByIdAsync(Guid productSellerId)
        {
            return await _productSellers.SingleOrDefaultAsync(model => model.Id == productSellerId);
        }


        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetProductSellerAsSelectItemList()
        {
            // Process

            var list = await _productSellers.AsNoTracking().Select(model => new SelectListItem
            {
                Value = model.Id.ToString(),
                Text = model.CompanyTitle
            }).ToListAsync();

            // Result
            return list;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var result = _productSellers.AsNoTracking().ToDataSourceResult(request);

            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<ProductSellerViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task EditByViewModelAsync(ProductSellerEditViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var productSeller = await _productSellers.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(productSeller, viewModel);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods
    }
}