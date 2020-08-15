using System;
using System.Threading.Tasks;
using Clinic.Core.Models.DocumentSaleProduct;

namespace Clinic.Service.Factories.Documents
{
    public interface IDocumentSaleProductFactory
    {
        Task<DocumentSaleProductEditViewModel> PrepareEditViewModelAsync(Guid documentSaleProductid);
    }
}