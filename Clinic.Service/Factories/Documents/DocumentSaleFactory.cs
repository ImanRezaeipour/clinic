using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Models.DocumentSale;
using Clinic.Service.Services.Documents;

namespace Clinic.Service.Factories.Documents
{
   public class DocumentSaleFactory : IDocumentSaleFactory
   {
        #region Private Fields

        private readonly IDocumentSaleService _documentSaleService;
       private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public DocumentSaleFactory(IDocumentSaleService documentSaleService, IMapper mapper)
       {
           _documentSaleService = documentSaleService;
           _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<DocumentSaleEditViewModel> GetForEditAsync(Guid documentSaleid)
       {
           var documentSale = await _documentSaleService.FindByIdAsync(documentSaleid);
           var viewModel = _mapper.Map<DocumentSaleEditViewModel>(documentSale);

           return viewModel;
        }

        #endregion Public Methods
    }
}
