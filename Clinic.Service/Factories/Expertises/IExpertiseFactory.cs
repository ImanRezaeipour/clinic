using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Expertise;

namespace Clinic.Service.Factories.Expertises
{
    public interface IExpertiseFactory
    {
        Task<ExpertiseEditViewModel> PrepareEditViewModelAsync(Guid expertiseId);
    }
}