namespace Clinic.Core.Models.Common
{
    /// <summary>
    /// </summary>
    public abstract class SearchRequest
    {
        #region Public Properties

        /// <summary>
        /// رشته جستجو 
        /// </summary>
        public string Term { get; set; }

        #endregion Public Properties
    }
}