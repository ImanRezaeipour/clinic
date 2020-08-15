using System.Threading.Tasks;
using Clinic.Core.Models.Account;

namespace Clinic.Service.Validations
{
    public interface IAccountValidation
    {
        Task<string> SignInAsync(LoginViewModel viewModel);
        Task<string> SignInValidationAsync(LoginViewModel viewModel);
    }
}