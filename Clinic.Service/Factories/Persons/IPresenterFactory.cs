using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Presenter;

namespace Clinic.Service.Factories.Persons
{
    public interface IPresenterFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="presentId"></param>
        /// <returns></returns>
        Task<PresenterEditViewModel> PrepareEditViewModelAsync(Guid presentId);
    }
}