using System;
using System.Threading.Tasks;
using Clinic.Core.Models.Statistic;

namespace Clinic.Service.Services.Statistics
{
    public interface IStatisticService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task CreateByViewModelAsync(StatisticCreateViewModel viewModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="statisticId"></param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid statisticId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task UpdateByViewModelAsync(StatisticEditViewModel viewModel);
    }
}