using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Document;

namespace Clinic.Service.Factories.Documents
{
    public interface IDocumentFactory
    {
        Task<DocumentEditViewModel> PrepareEditViewModelAsync(Guid documentId);
    }
}