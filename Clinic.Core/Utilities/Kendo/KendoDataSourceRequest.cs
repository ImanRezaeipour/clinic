using System.Collections.Generic;

namespace Clinic.Core.Utilities.Kendo
{
    /// <summary>
    /// Describes a Kendo Datasource request.
    /// </summary>
    public class KendoDataSourceRequest
    {
        #region Public Properties

        public IEnumerable<KendoAggregator> Aggregates { get; set; }

        /// <summary>
        /// Specifies the requested filter.
        /// </summary>
        public KendoFilter Filter { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// Specifies how many items to skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Specifies the requested sort order.
        /// </summary>
        public IEnumerable<KendoSort> Sort { get; set; }

        /// <summary>
        /// Specifies how many items to take.
        /// </summary>
        public int Take { get; set; }

        #endregion Public Properties
    }
}