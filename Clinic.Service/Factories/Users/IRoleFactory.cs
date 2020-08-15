using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Role;

namespace Clinic.Service.Factories.Users
{
    public interface IRoleFactory
    {
        Task<RoleEditViewModel> PrepareEditViewModelAsync(Guid roleId);
        Task<RoleListViewModel> PrepareListViewModelAsync(RoleSearchRequest request, bool isCurrentUser = false, Guid? userId = default(Guid?));
    }
}