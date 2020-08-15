using System.Threading.Tasks;

namespace Clinic.Service.Validations
{
    public interface IPatientValidator
    {
        Task<string> CreateValidationAsync(string nationalCode);
    }
}