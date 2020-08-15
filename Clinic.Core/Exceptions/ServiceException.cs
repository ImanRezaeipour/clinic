using System;
using System.Runtime.Serialization;

namespace Clinic.Core.Exceptions
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceException : Exception
    {
        #region Public Constructors

        public ServiceException()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}