using AutoMapper;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Models.Document;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Clinic.Service.Services.Documents
{
    /// <summary>
    ///
    /// </summary>
    public class DocumentService : IDocumentService
    {
        #region Private Fields

        private readonly IDbSet<Document> _documentRepository;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="httpContextManager"></param>
        public DocumentService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _documentRepository = unitOfWork.Set<Document>();
            _httpContextManager = httpContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(DocumentCreateViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var document = _mapper.Map<Document>(viewModel);
            _documentRepository.Add(document);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task DeleteById(Guid documentId)
        {
            var document = await FindByIdAsync(documentId);
            _documentRepository.Remove(document);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task EditByViewModelAsync(DocumentEditViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var documet = await FindByIdAsync(viewModel.Id);
            _mapper.Map(viewModel, documet);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<Document> FindByIdAsync(Guid documentId)
        {
            return await _documentRepository.SingleOrDefaultAsync(model => model.Id == documentId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var dataSource = _documentRepository.AsNoTracking().ToDataSourceResult(request);
            return new KendoDataSourceResult
            {
                Data = _mapper.Map<IList<DocumentViewModel>>(dataSource.Data),
                Total = dataSource.Total,
                Aggregates = dataSource.Aggregates
            };
        }

        #endregion Public Methods
    }
}