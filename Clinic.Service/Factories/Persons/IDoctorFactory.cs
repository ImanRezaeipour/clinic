using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Doctor;

namespace Clinic.Service.Factories.Persons
{
    public interface IDoctorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        Task<DoctorEditViewModel> PrepareEditViewModelAsync(Guid doctorId);
    }
}