using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Addresses;
using Clinic.Core.Models.Address;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Locations
{
    /// <summary>
    /// </summary>
    public class AddressService :IAddressService
    {

        #region Private Fields

        private readonly IDbSet<Address> _addressStore;
        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="mapper"></param>
        ///  <param name="unitOfWork"></param>
        /// <param name="httpContextManager"></param>
        /// <param name="listManager"></param>
        public AddressService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _addressStore = unitOfWork.Set<Address>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(AddressCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentException(nameof(viewModel));

            // Process
            var address = _mapper.Map<Address>(viewModel);
             _addressStore.Add(address);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid addressId)
        {
            // Pocess
            var address = await _addressStore.FirstOrDefaultAsync(model => model.Id == addressId);
             _addressStore.Remove(address);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteByUserIdAsync(Guid userId)
        {
            // Pocess
            var address = await _addressStore.FirstOrDefaultAsync(model => model.CreatedById == userId);
             _addressStore.Remove(address);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public async Task<AddressEditViewModel> GetForEditAsync(Guid addressId)
        {
            // Process
            var address = await _addressStore.FirstOrDefaultAsync(model => model.Id == addressId);
            var viewModel = _mapper.Map<AddressEditViewModel>(address);

            // Result
            return viewModel;
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(AddressEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var address = await _addressStore.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
           _mapper.Map(viewModel, address);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods

    }
}