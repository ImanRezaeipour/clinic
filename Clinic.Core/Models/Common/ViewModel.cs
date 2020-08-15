
using Clinic.Core.Types;

namespace Clinic.Core.Models.Common
{
    /// <summary>
    /// </summary>
    public abstract class ViewModel
    {
        #region Public Properties

        /// <summary>
        /// نوع اکشن انجام شده 
        /// </summary>
        public AuditType Audit { get; set; }

        #endregion Public Properties
    }
}