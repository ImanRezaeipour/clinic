using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Doctors;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Expertise;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Expertises
{
    public class ExpertiseService : IExpertiseService
    {
        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<Expertise> _expertises;


        #endregion

        #region Public Constructors

        public ExpertiseService(IHttpContextManager httpContextManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _httpContextManager = httpContextManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _expertises = unitOfWork.Set<Expertise>();
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(ExpertiseCreateViewModel viewModel)
        {
            if(viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var expertiseMap = _mapper.Map<Expertise>(viewModel);
             _expertises.Add(expertiseMap);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        public async Task CreateByModelAsync(Expertise expertise)
        {
            if (expertise == null)
                throw new ArgumentException();

            _expertises.Add(expertise);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expertiseId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid expertiseId)
        {
            var expertise = await FindByIdAsync(expertiseId);
             _expertises.Remove(expertise);
        await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var result =_expertises.AsNoTracking().ToDataSourceResult(request);

            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<ExpertiseViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        public async Task<Expertise> FindByIdAsync(Guid expertiseId)
        {
            return await _expertises.SingleOrDefaultAsync(model => model.Id == expertiseId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task EditByViewModelAsync(ExpertiseEditViewModel viewModel)
        {
            // Check
            if(viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var expertise =await _expertises.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel,expertise);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

            // Result
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetExpertiseSelectListItemAsync()
        {
            var list = await _expertises.Select(record => new SelectListItem
            {
                Value = record.Id.ToString(),
                Text = record.ExpertiseName
            }).ToListAsync();

            var selectList = _mapper.Map<IList<SelectListItem>>(list);
            return selectList;
        }

        #endregion

    }
}
