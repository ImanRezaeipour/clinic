using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Models.Presenter;
using Clinic.Service.Services.Persons;

namespace Clinic.Service.Factories.Persons
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly IMapper _mapper;
        private readonly IPresenterService _presenterService;

        public PresenterFactory(IMapper mapper, IPresenterService presenterService)
        {
            _mapper = mapper;
            _presenterService = presenterService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="presentId"></param>
        /// <returns></returns>
        public async Task<PresenterEditViewModel> PrepareEditViewModelAsync(Guid presentId)
        {
            var present = await _presenterService.FindByIdAsync(presentId);

            var viewModel = present != null ? _mapper.Map<PresenterEditViewModel>(present) : new PresenterEditViewModel();

            return viewModel;
        }

    }
}
