using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Presenters;
using Clinic.Core.Models.Common;
using Clinic.Core.Models.Presenter;
using Clinic.Core.Utilities.Kendo;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Persons
{
    public class PresenterService :  IPresenterService
    {

        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<Presenter> _presenters;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public PresenterService(IMapper mapper, IHttpContextManager httpContextManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _httpContextManager = httpContextManager;
            _unitOfWork = unitOfWork;
            _presenters = unitOfWork.Set<Presenter>();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(PresenterCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var present = _mapper.Map<Presenter>(viewModel);
            _presenters.Add(present);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="presentId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid presentId)
        {
            var present = await _presenters.FirstOrDefaultAsync(model => model.Id == presentId);
             _presenters.Remove(present);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetPresenterAsSelectListItem()
        {
            // Process
            var list = await _presenters.AsNoTracking().Select(model => new SelectListItem
            {
                Value = model.Id.ToString(),
                Text = model.LastName
            }).ToListAsync();

            // Result
            return list;
        }

        public async Task<Presenter> FindByIdAsync(Guid presenterId)
        {
            return await _presenters.SingleAsync(model => model.Id == presenterId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KendoDataSourceResult> ListByRequestAsync(KendoDataSourceRequest request)
        {
            var present = _presenters.AsNoTracking().ToDataSourceResult(request);
            return new KendoDataSourceResult
            {
                Data = _mapper.Map<List<PresenterViewModel>>(present.Data),
                Total = present.Total,
                Aggregates = present.Aggregates
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task EditByViewModelAsync(PresenterEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var present = await _presenters.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, present);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        #endregion Public Methods
    }
}