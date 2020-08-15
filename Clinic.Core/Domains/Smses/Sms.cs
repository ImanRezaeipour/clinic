using System;
using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Users;

namespace Clinic.Core.Domains.Smses
{
    /// <summary>
    ///
    /// </summary>
    public class Sms : BaseMessage
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