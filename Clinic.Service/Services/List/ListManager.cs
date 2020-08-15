using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.Core.Models.Common;

namespace Clinic.Service.Services.List
{
    public class ListManager : IListManager
    {

        #region Public Methods

        public async Task<IList<SelectListItem>> GetActiveListAsync()
        {
            var active = new List<SelectListItem>
            {
                new SelectListItem{Value = string.Empty,Text = "همه"},
                new SelectListItem { Value = bool.TrueString,Text = "فعال"},
                new SelectListItem { Value = bool.FalseString,Text = "غیرفعال"},
            };
            return active;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetActivityLogSortMemberListAsync()
        {
            // Process
            var listMember = new List<SelectListItem>
            {
                new SelectListItem { Value =SortMember.CreatedOn, Text = "تاریخ درج" },
                new SelectListItem { Value = SortMember.ModifiedOn, Text = "تاریخ آخرین تغییر" }
            };

            // Result
            return listMember;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetAddressSortMemberListAsync()
        {
            // Process
            var sortList = new List<SelectListItem>
            {
                new SelectListItem { Value =SortMember.CreatedOn, Text = "تاریخ درج" },
                new SelectListItem { Value = SortMember.ModifiedOn, Text = "تاریخ آخرین تغییر" }
            };
            // Result
            return sortList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetAuditLogSortMemberListAsync()
        {
            // Process
            var sortList = new List<SelectListItem>
            {
                new SelectListItem { Value =SortMember.CreatedOn, Text = "تاریخ درج" },
                new SelectListItem { Value = SortMember.ModifiedOn, Text = "تاریخ آخرین تغییر" }
            };
            // Result
            return sortList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetSmsSortMemberListAsync()
        {
            // Process
            var sortList = new List<SelectListItem>
            {
                new SelectListItem { Value =SortMember.CreatedOn, Text = "تاریخ درج" },
                new SelectListItem { Value = SortMember.ModifiedOn, Text = "تاریخ آخرین تغییر" }
            };
            // Result
            return sortList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetStatisticSortMemberListAsync()
        {
            // Process
            var sortList = new List<SelectListItem>
            {
                new SelectListItem { Value =SortMember.CreatedOn, Text = "تاریخ درج" },
                new SelectListItem { Value = SortMember.ModifiedOn, Text = "تاریخ آخرین تغییر" }
            };
            // Result
            return sortList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetEmailSortMemberListAsync()
        {
            // Process
            var sortList = new List<SelectListItem>
            {
                new SelectListItem { Value =SortMember.CreatedOn, Text = "تاریخ درج" },
                new SelectListItem { Value = SortMember.ModifiedOn, Text = "تاریخ آخرین تغییر" }
            };
            // Result
            return sortList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetIsActiveListAsync()
        {
            var sortDirectionList = new List<SelectListItem>
            {
                new SelectListItem { Value = string.Empty, Text = "همه" },
                new SelectListItem { Value = bool.TrueString, Text = "فعال" },
                new SelectListItem { Value =bool.FalseString, Text = "غیرفعال" }
            };

            return sortDirectionList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetIsBanListAsync()
        {
            var sortDirectionList = new List<SelectListItem>
            {
                new SelectListItem { Value = string.Empty, Text = "همه" },
                new SelectListItem { Value = bool.TrueString, Text = "قفل شده" },
                new SelectListItem { Value = bool.FalseString, Text = "قفل نشده" }
            };

            return sortDirectionList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetIsVerifyListAsync()
        {
            var sortDirectionList = new List<SelectListItem>
            {
                new SelectListItem { Value = string.Empty, Text = "همه" },
                new SelectListItem { Value = bool.TrueString, Text = "تائید شده" },
                new SelectListItem { Value = bool.FalseString, Text = "تائید نشده" }
            };

            return sortDirectionList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetPageSizeListAsync()
        {
            var pageSizeList = new List<SelectListItem>
            {
                new SelectListItem { Value = PageSize.Count10.ToString(), Text = "۱۰" },
                new SelectListItem { Value = PageSize.Count20.ToString(), Text = "۲۰" },
                new SelectListItem { Value = PageSize.Count30.ToString(), Text = "۳۰" },
                new SelectListItem { Value = PageSize.Count50.ToString(), Text = "۵۰" },
                new SelectListItem { Value = PageSize.Count100.ToString(), Text = "۱۰۰" }
            };

            return pageSizeList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetRoleSortMemberListAsync()
        {
            // Process
            var sortList = new List<SelectListItem>
            {
                new SelectListItem { Value =SortMember.CreatedOn, Text = "تاریخ درج" },
                new SelectListItem { Value = SortMember.ModifiedOn, Text = "تاریخ آخرین تغییر" }
            };

            // Result
            return sortList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetSortDirectionListAsync()
        {
            var sortDirectionList = new List<SelectListItem>
            {
                new SelectListItem { Value = SortDirection.Asc, Text = "صعودی" },
                new SelectListItem { Value = SortDirection.Desc, Text = "نزولی" }
            };

            return sortDirectionList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetStateListAsync()
        {
            var stateList = new List<SelectListItem>
            {
                new SelectListItem {Text = "همه",Value = "-1"},
                new SelectListItem {Text = "تائید شده",Value = "1"},
                new SelectListItem {Text = "درحال بررسی",Value = "2"},
                new SelectListItem {Text = "رد شده",Value = "3"},
            };
            return stateList;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SelectListItem>> GetUserSortMemberListAsync()
        {
            // Process
            var sortList = new List<SelectListItem>
            {
                new SelectListItem { Value =SortMember.CreatedOn, Text = "تاریخ درج" },
                new SelectListItem { Value = SortMember.ModifiedOn, Text = "تاریخ آخرین تغییر" }
            };
            // Result
            return sortList;
        }

        #endregion Public Methods

    }
}