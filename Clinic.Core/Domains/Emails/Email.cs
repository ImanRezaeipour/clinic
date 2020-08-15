using System;
using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Users;

namespace Clinic.Core.Domains.Emails
{
    /// <summary>
    /// </summary>
    public class Email : BaseMessage
    {

        #region Public Properties

        /// <summary>
        /// </summary>
        public virtual User RecievedBy { get; set; }

        /// <summary>
        /// </summary>
        public virtual Guid? RecievedById { get; set; }

        /// <summary>
        /// </summary>
        public virtual User SentBy { get; set; }

        /// <summary>
        /// </summary>
        public virtual Guid? SentById { get; set; }

        #endregion Public Properties

    }
}