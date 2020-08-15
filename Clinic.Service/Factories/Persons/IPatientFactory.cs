using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Doctor;
using Clinic.Core.Models.Patient;

namespace Clinic.Service.Factories.Persons
{
    public interface IPatientFactory
    {
        ///  <summary>
        /// 
        ///  </summary>
        /// <param name="nationalCode"></param>
        /// <returns></returns>
        Task<PatientEditViewModel> PrepareEditViewModelAsync(string nationalCode);
    }
}