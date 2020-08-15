using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core.Domains.Statistics;
using Clinic.Core.Models.Statistic;
using Clinic.Data.DbContexts;
using Clinic.Service.Services.HttpContext;

namespace Clinic.Service.Services.Statistics
{
    /// <summary>
    ///
    /// </summary>
    public class StatisticService :  IStatisticService
    {
        #region Private Fields

        private readonly IHttpContextManager _httpContextManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<Statistic> _statistics;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="mapper"></param>
        ///  <param name="unitOfWork"></param>
        /// <param name="httpContextManager"></param>
        /// <param name="listManager"></param>
        public StatisticService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextManager httpContextManager)
        {
            _statistics = unitOfWork.Set<Statistic>();
            _unitOfWork = unitOfWork;
            _httpContextManager = httpContextManager;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task CreateByViewModelAsync(StatisticCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var statistic = _mapper.Map<Statistic>(viewModel);
             _statistics.Add(statistic);
             await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="statisticId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(Guid statisticId)
        {
            // Process
            var statistic = await _statistics.FirstOrDefaultAsync(model => model.Id == statisticId);
            _statistics.Remove(statistic);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task UpdateByViewModelAsync(StatisticEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            // Process
            var statistic = await _statistics.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
             _mapper.Map(viewModel, statistic);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _httpContextManager.CurrentUserId());
            
        }

        #endregion Public Methods
    }
}