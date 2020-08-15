using System.Threading.Tasks;
using Clinic.Core.Models.Role;
using Clinic.Service.Services.Roles;

namespace Clinic.Service.Validations
{
    /// <summary>
    ///
    /// </summary>
    public class RoleValidation : IRoleValidation
    {
        private readonly IRoleService _roleService;

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="roleService"></param>
        public RoleValidation(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<string> CreateValidationAsync(RoleCreateViewModel viewModel)
        {
            var exist = await _roleService.IsExistNameAsync(viewModel.Name);
            return exist ? "این گروه  قبلا در سیستم ثبت شده است" : null;
        }

        public async Task<string> UpdateValidationAsync(RoleEditViewModel viewModel)
        {
            var exist = await _roleService.IsExistNameAsync(viewModel.Name);
            if (exist == false)
            {
                return "این گروه  قبلا در سیستم ثبت شده است";
            }

            //if (!await _roleService.IsInDb(viewModel.Id))
            //    return HttpNotFound();

            return null;
        }

    }
}
