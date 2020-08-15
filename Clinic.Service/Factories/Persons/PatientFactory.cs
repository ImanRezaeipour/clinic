using AutoMapper;
using Clinic.Service.Services.Persons;
using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Patient;

namespace Clinic.Service.Factories.Persons
{
    public class PatientFactory : IPatientFactory
    {
        #region Private Fields

        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public PatientFactory(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        ///  <summary>
        /// 
        ///  </summary>
        /// <param name="nationalCode"></param>
        /// <returns></returns>
        public async Task<PatientEditViewModel> PrepareEditViewModelAsync(string nationalCode)
        {
            // Process
            var doctor = await _patientService.FindByNationalcodeAsync(nationalCode);
            var viewModel = _mapper.Map<PatientEditViewModel>(doctor);

            // Result
            return viewModel;
        }

        #endregion Public Methods
    }
}