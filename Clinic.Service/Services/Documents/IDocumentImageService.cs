using System;
using System.Threading.Tasks;
using Clinic.Core.Domains.Documents;
using Clinic.Core.Models.DocumentImage;
using Clinic.Core.Utilities.Kendo;

namespace Clinic.Service.Services.Documents
{
    public interface IDocumentImageService
    {
        Task CreateByViewModelAsync(DocumentImageCreateViewModel viewModel);
        Task DeleteByIdAsync(Guid documentImageid);
        Task EditByViewModelAsync(DocumentImageEditViewModel viewModel);
        Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request);
        Task<DocumentImage> FindByIdAsync(Guid documentImageId);
    }
}