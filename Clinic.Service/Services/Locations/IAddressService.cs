using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Address;

namespace Clinic.Service.Services.Locations
{
    public interface IAddressService
    {
        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(AddressCreateViewModel viewModel);

        /// <summary>
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid addressId);

        /// <summary>
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        Task<AddressEditViewModel> GetForEditAsync(Guid addressId);


        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task UpdateByViewModelAsync(AddressEditViewModel viewModel);

        Task DeleteByUserIdAsync(Guid userId);
    }
}