using AutoMapper;
using Clinic.Core.Models.Doctor;
using Clinic.Service.Services.Expertises;
using Clinic.Service.Services.Persons;
using System;
using System.Threading.Tasks;

namespace Clinic.Service.Factories.Persons
{
    public class DoctorFactory : IDoctorFactory
    {
        #region Private Fields

        private readonly IDoctorService _doctorService;
        private readonly IExpertiseService _expertiseService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public DoctorFactory(IDoctorService doctorService, IMapper mapper, IExpertiseService expertiseService)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _expertiseService = expertiseService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public async Task<DoctorEditViewModel> PrepareEditViewModelAsync(Guid doctorId)
        {
            // Process
            var doctor = await _doctorService.FindByIdAsync(doctorId);
            var viewModel = _mapper.Map<DoctorEditViewModel>(doctor);
            viewModel.ExpertiseList = await _expertiseService.GetExpertiseSelectListItemAsync();

            // Result
            return viewModel;
        }

        #endregion Public Methods
    }
}