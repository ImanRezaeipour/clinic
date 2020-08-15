using AutoMapper;
using Clinic.Core.Domains.Doctors;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Doctor;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.Expertises;
using Clinic.Service.Services.HttpContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Service.Services.Persons
{
    public class DoctorService : IDoctorService
    {
        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<Doctor> _doctors;
        private readonly IExpertiseService _expertiseService;

        #endregion Private Fields

        #region Public Constructors

        public DoctorService(IHttpContextManager httpContextManager, IUnitOfWork unitOfWork, IMapper mapper, IExpertiseService expertiseService)
        {
            _httpContextManager = httpContextManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _expertiseService = expertiseService;
            _doctors = unitOfWork.Set<Doctor>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(DoctorCreateViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var doctorMap = _mapper.Map<Doctor>(viewModel);
            Guid isGuid;
            Guid.TryParse(viewModel.ExpertiseId, out isGuid);
            if (isGuid != Guid.Empty)
                doctorMap.ExpertiseId = Guid.Parse(viewModel.ExpertiseId);
            else
                doctorMap.Expertise = new Expertise
                {
                    ExpertiseName = viewModel.ExpertiseId
                };


            _doctors.Add(doctorMap);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid doctorId)
        {
            var doctor = await FindByIdAsync(doctorId);
            _doctors.Remove(doctor);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public async Task<Doctor> FindByIdAsync(Guid doctorId)
        {
            return await _doctors.SingleAsync(model => model.Id == doctorId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var result = _doctors.AsNoTracking().ToDataSourceResult(request);

            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<DoctorViewModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task EditByViewModelAsync(DoctorEditViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var doctor = await QueryableExtensions.FirstOrDefaultAsync<Doctor>(_doctors, model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, doctor);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetDoctorAsSelectItemListAsync()
        {
            // Process
            var list = await _doctors.Select(model => new SelectListItem
            {
                Value = model.Id.ToString(),
                Text = model.LastName
            }).ToListAsync();

            // Result
            return list;
        }
    }
     
    #endregion Public Methods
}