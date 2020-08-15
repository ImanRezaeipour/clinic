using AutoMapper;
using Clinic.Core.Models.Document;
using Clinic.Service.Services.Documents;
using System;
using System.Threading.Tasks;

namespace Clinic.Service.Factories.Documents
{
    /// <summary>
    ///
    /// </summary>
    public class DocumentFactory : IDocumentFactory
    {
        #region Private Fields

        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentService"></param>
        /// <param name="mapper"></param>
        public DocumentFactory(IDocumentService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<DocumentEditViewModel> PrepareEditViewModelAsync(Guid documentId)
        {
            var document = await _documentService.FindByIdAsync(documentId);
            var viewModel = _mapper.Map<DocumentEditViewModel>(document);

            return viewModel;
        }

        #endregion Public Methods
    }
}