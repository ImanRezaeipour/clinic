using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class PagingExtensions
    {
        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex == 0)
                pageIndex = 1;
            if (pageSize == 0)
                pageSize = PageSize.All;
            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;

            source = source.Skip(skip).Take(take);
            var result =await source.ToListAsync();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public  static async Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> source, BaseSearchRequest request)
        {
            return await  source.ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        #endregion Public Methods
    }
}