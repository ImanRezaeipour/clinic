using System.Runtime.Serialization;

namespace Clinic.Core.Utilities.Kendo
{
    /// <summary>
    /// Represents a sort expression of Kendo DataSource.
    /// </summary>
    [DataContract]
    public class KendoSort
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the sort direction. Should be either "asc" or "desc".
        /// </summary>
        [DataMember(Name = "dir")]
        public string Dir { get; set; }

        /// <summary>
        /// Gets or sets the name of the sorted field (property).
        /// </summary>
        [DataMember(Name = "field")]
        public string Field { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Converts to form required by Dynamic Linq e.g. "Field1 desc"
        /// </summary>
        public string ToExpression()
        {
            return Field + " " + Dir;
        }

        #endregion Public Methods
    }
}