using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Models.Common;

namespace Clinic.Service.Services.List
{
    public interface IListManager
    {
        Task<IList<SelectListItem>> GetActiveListAsync();
        Task<IList<SelectListItem>> GetPageSizeListAsync();
        Task<IList<SelectListItem>> GetSortDirectionListAsync();
        Task<IList<SelectListItem>> GetStateListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetIsActiveListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetIsBanListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetIsVerifyListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetUserSortMemberListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetRoleSortMemberListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetEmailSortMemberListAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetActivityLogSortMemberListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetAddressSortMemberListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetAuditLogSortMemberListAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetSmsSortMemberListAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectListItem>> GetStatisticSortMemberListAsync();
    }
}