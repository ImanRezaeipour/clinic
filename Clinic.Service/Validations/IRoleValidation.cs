using System.Threading.Tasks;
using Clinic.Core.Models.Role;

namespace Clinic.Service.Validations
{
    public interface IRoleValidation
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<string> CreateValidationAsync(RoleCreateViewModel viewModel);

        Task<string> UpdateValidationAsync(RoleEditViewModel viewModel);
    }
}