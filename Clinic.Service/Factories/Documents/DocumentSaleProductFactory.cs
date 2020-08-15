using AutoMapper;
using Clinic.Core.Models.DocumentSaleProduct;
using Clinic.Service.Services.Documents;
using System;
using System.Threading.Tasks;

namespace Clinic.Service.Factories.Documents
{
    public class DocumentSaleProductFactory : IDocumentSaleProductFactory
    {
        #region Private Fields

        private readonly IDocumentSaleProductService _documentSaleProductService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public DocumentSaleProductFactory(IDocumentSaleProductService documentSaleProductService, IMapper mapper)
        {
            _documentSaleProductService = documentSaleProductService;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<DocumentSaleProductEditViewModel> PrepareEditViewModelAsync(Guid documentSaleProductid)
        {
            var documentSaleProduct = await _documentSaleProductService.FindByIdAsync(documentSaleProductid);
            var viewModel = _mapper.Map<DocumentSaleProductEditViewModel>(documentSaleProduct);

            return viewModel;
        }

        #endregion Public Methods
    }
}