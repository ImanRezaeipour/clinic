using System;
using System.Threading.Tasks;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Models.Document;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Documents
{
    public interface IDocumentService
    {
        Task CreateByViewModelAsync(DocumentCreateViewModel viewModel);
        Task EditByViewModelAsync(DocumentEditViewModel viewModel);
        Task DeleteById(Guid documentId);
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        Task<Document> FindByIdAsync(Guid documentId);
    }
}