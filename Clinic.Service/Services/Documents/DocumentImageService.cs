using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Models.DocumentImage;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Documents
{
    public class DocumentImageService :  IDocumentImageService
    {

        #region Private Fields

        private readonly IDbSet<DocumentImage> _documentImages;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public DocumentImageService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _documentImages = unitOfWork.Set<DocumentImage>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task CreateByViewModelAsync(DocumentImageCreateViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            var documentImage = _mapper.Map<DocumentImage>(viewModel);
            _documentImages.Add(documentImage);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        public async Task DeleteByIdAsync(Guid documentImageId)
        {
            var documentImage = await FindByIdAsync(documentImageId);
            var deletdocumentImage = _documentImages.Remove(documentImage);
            var result = await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        public async Task<DocumentImage> FindByIdAsync(Guid documentImageId)
        {
            return await _documentImages.SingleOrDefaultAsync(model => model.Id == documentImageId);
        }

        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            if (request == null)
                throw new ArgumentNullException();

            var result = _documentImages.AsNoTracking().ToDataSourceResult(request);
            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<DocumentImageViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        public async Task EditByViewModelAsync(DocumentImageEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException();

            // Process
            var documentImage = FindByIdAsync(viewModel.Id);
            await _mapper.Map(viewModel, documentImage);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods
    }
}