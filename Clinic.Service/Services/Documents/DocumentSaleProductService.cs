using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Domains.Sales;
using Clinic.Core.Models.DocumentSaleProduct;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Documents
{
    public class DocumentSaleProductService :  IDocumentSaleProductService
    {

        #region Private Fields

        private readonly IDbSet<SaleProduct> _documentSaleProducts;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public DocumentSaleProductService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _documentSaleProducts = unitOfWork.Set<SaleProduct>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task CreateByViewModelAsync(DocumentSaleProductCreateViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            var documentSaleProduct = _mapper.Map<SaleProduct>(viewModel);
            _documentSaleProducts.Add(documentSaleProduct);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        public async Task DeleteByIdAsync(Guid documentSaleProductId)
        {
            var documentSaleProduct = await _documentSaleProducts.FirstOrDefaultAsync(model => model.Id == documentSaleProductId);
            _documentSaleProducts.Remove(documentSaleProduct);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        public async Task<SaleProduct> FindByIdAsync(Guid documentSaleProductId)
        {
            return await _documentSaleProducts.SingleOrDefaultAsync(model => model.Id == documentSaleProductId);
        }

        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            if (request == null)
                throw new ArgumentNullException();

            var result = _documentSaleProducts.AsNoTracking().ToDataSourceResult(request);
            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<DocumentSaleProductViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        public async Task EditByViewModelAsync(DocumentSaleProductEditViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            var documentSaleProduct = _documentSaleProducts.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
           await _mapper.Map(viewModel, documentSaleProduct);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
            
        }

        #endregion Public Methods
    }
}