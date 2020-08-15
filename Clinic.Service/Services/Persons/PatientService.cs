// file:	Services\PatientService.cs
//
// summary:	Implements the patient service class

using AutoMapper;
using Clinic.Core.Domains.Patients;
using Clinic.Core.Models.Patient;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Core.Models.Common;

namespace Clinic.Service.Services.Persons
{
    /// <summary>   A patient service. </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>

    public class PatientService : IPatientService
    {

        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<Patient> _patients;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContextManager"></param>
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextManager httpContextManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextManager = httpContextManager;
            _patients = unitOfWork.Set<Patient>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(PatientCreateViewModel viewModel)
        {
            //  Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            //  Process
            var patient = _mapper.Map<Patient>(viewModel);
            _patients.Add(patient);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid patientId)
        {
            //  Process
            var patient = await _patients.FirstOrDefaultAsync(model => model.Id == patientId);
            _patients.Remove(patient);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var res = _patients.AsNoTracking().ToDataSourceResult(request);

            return new KendoDataSourceResult
            {
                Data = _mapper.Map<IList<PatientViewModel>>(res.Data),
                Total = res.Total,
                Aggregates = res.Aggregates
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(PatientEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var patient = await _patients.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, patient);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
        }

        public async Task<Patient> FindByIdAsync(Guid patientId)
        {
            return await _patients.SingleAsync(model => model.Id == patientId);
        }
 public async Task<Patient> FindByNationalcodeAsync(string nationalCode)
        {
            return await _patients.SingleAsync(model => model.NationalCode == nationalCode);
        }

        public async Task<bool> IsExistNationalCodeAsync(string nationalCode)
        {
            return await _patients.AnyAsync(model => model.NationalCode == nationalCode);
        }


        #endregion Public Methods

        public async Task<IList<SelectListItem>> GetAsSelectListItem()
        {
            var list = await _patients.AsNoTracking().Select(model => new SelectListItem
            {
                Value = model.Id.ToString(),
                Text = model.FirstName + " " + model.LastName
            }).ToListAsync();

            return list;
        }

    }
}