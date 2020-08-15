using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Models.Expertise;
using Clinic.Service.Services.Expertises;

namespace Clinic.Service.Factories.Expertises
{
    public class ExpertiseFactory : IExpertiseFactory
    {
        private readonly IExpertiseService _expertiseService;
        private readonly IMapper _mapper;

        public ExpertiseFactory(IExpertiseService expertiseService, IMapper mapper)
        {
            _expertiseService = expertiseService;
            _mapper = mapper;
        }
        public async Task<ExpertiseEditViewModel> PrepareEditViewModelAsync(Guid expertiseId)
        {
            var expertise = await _expertiseService.FindByIdAsync(expertiseId);
            var viewModel = _mapper.Map<ExpertiseEditViewModel>(expertise);

            return viewModel;
        }
    }
}
