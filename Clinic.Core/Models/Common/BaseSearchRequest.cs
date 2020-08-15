namespace Clinic.Core.Models.Common
{
    /// <summary>
    /// کلاس پایه برای اطلاعات لازم برای جستجو و مرتب سازی 
    /// IPagedList<T>
    /// </summary>
    public abstract class BaseSearchRequest : SearchRequest
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// تعداد در صفحه 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// جهت مرتب سازی 
        /// </summary>
        public string SortDirection { get; set; }

        /// <summary>
        /// نام فیلد جاری برای مرتب سازی 
        /// </summary>
        public string SortMember { get; set; }

        /// <summary>
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// تعداد کل داده ها 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFirstPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLastPage { get; set; }


        #endregion Public Properties
    }
}