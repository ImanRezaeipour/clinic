using System;
using System.Threading.Tasks;
using Clinic.Core.Models.DocumentSale;

namespace Clinic.Service.Factories.Documents
{
    public interface IDocumentSaleFactory
    {
        Task<DocumentSaleEditViewModel> GetForEditAsync(Guid documentSaleid);
    }
}