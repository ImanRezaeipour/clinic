using AutoMapper;
using Clinic.Core.Domains.Products;
using Clinic.Core.Models.Product;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Core.Models.Common;

namespace Clinic.Service.Services.Products
{
    public class ProductService : IProductService
    {

        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<Product> _products;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ProductService(IHttpContextManager httpContextManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _httpContextManager = httpContextManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _products = unitOfWork.Set<Product>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(ProductCreateViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var productMap = _mapper.Map<Product>(viewModel);
            var product = _products.Add(productMap);
            product.IsActive = true;
            product.IsProduct = true;
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid productId)
        {
            var hasChild = await HasChildByParentIdAsync(productId);
            if(hasChild)
                throw new ArgumentNullException("دارای زیرمجموعه میباشد");

            var product = await _products.FirstOrDefaultAsync(model => model.Id == productId);
            _products.Remove(product);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        public async Task<bool> HasChildByParentIdAsync(Guid parentId)
        {
            return await _products.AnyAsync(model => model.ParentId == parentId);
        }

        public async Task<Product> FindByIdAsync(Guid productId)
        {
            return await _products.SingleOrDefaultAsync(model => model.Id == productId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var result = _products.AsNoTracking().Where(product => product.IsProduct == true ).ToDataSourceResult(request);

            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<ProductViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ProductViewModel>> TreeAsync()
        {
            var list = await _products.Where(m => m.IsActive == true).ToListAsync();
            return  _mapper.Map<List<ProductViewModel>>(list);

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task EditByViewModelAsync(ProductEditViewModel viewModel)
        {
            if (viewModel == null )
                throw new ArgumentNullException(nameof(viewModel));

            if(viewModel.Id == viewModel.ParentId)
                throw new ArgumentException("زیر مجموعه درست انتخاب نشده");

            var product = await _products.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, product);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        public async Task<IList<SelectListItem>> GetAsSelectItemListAsync()
        {
            return await _products.Select(model => new SelectListItem
            {
                Value = model.Id.ToString(),
                Text = model.Title
            }).ToListAsync();
        }

        public async Task<decimal?> GetPriceByIdAsync(Guid productId)
        {
            return await _products.Where(product => product.Id == productId).Select(product => product.UnitPrice).SingleOrDefaultAsync();
        }

        #endregion Public Methods

    }
}