using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Service.Services.Persons;

namespace Clinic.Service.Validations
{
    public class PatientValidator : IPatientValidator
    {
        private readonly IPatientService _patientService;

        public PatientValidator(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<string> CreateValidationAsync(string nationalCode)
        {
            var user = await _patientService.IsExistNationalCodeAsync(nationalCode);
            if (user)
            {
                return "کد ملی تکرار است";
            }
            return null;

        }
    }
}
