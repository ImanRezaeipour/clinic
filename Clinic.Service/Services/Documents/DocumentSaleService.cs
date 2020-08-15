using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Domains.Sales;
using Clinic.Core.Models.DocumentSale;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Documents
{
    public class DocumentSaleService : IDocumentSaleService
    {

        #region Private Fields

        private readonly IDbSet<Sale> _documentSales;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public DocumentSaleService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _documentSales = unitOfWork.Set<Sale>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task CreateByViewModelAsync(DocumentSaleCreateViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            var documentSale = _mapper.Map<Sale>(viewModel);
            _documentSales.Add(documentSale);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        public async Task DeleteByIdAsync(Guid documentSaleId)
        {
            var documentSale = await _documentSales.FirstOrDefaultAsync(model => model.Id == documentSaleId);
            _documentSales.Remove(documentSale);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            if (request == null)
                throw new ArgumentNullException();

            var result = _documentSales.AsNoTracking().ToDataSourceResult(request);
            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<DocumentSaleViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        public async Task<Sale> FindByIdAsync(Guid documentSaleId)
        {
            return await _documentSales.SingleOrDefaultAsync(model => model.Id == documentSaleId);
        }

        public async Task EditByViewModelAsync(DocumentSaleEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException();

            // Process
            var documentSale = _documentSales.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
           await _mapper.Map(viewModel, documentSale);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods
    }
}