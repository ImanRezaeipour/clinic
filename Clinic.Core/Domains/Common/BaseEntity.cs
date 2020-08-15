using System;

namespace Clinic.Core.Domains.Common
{
    /// <summary>
    ///     نشان دهنده موجودیت پایه
    /// </summary>
    public abstract class BaseEntity : Entity
    {
        #region Properties

        /// <summary>
        ///     شناسه یکتای موجودیت
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        ///     gets or sets TimeStamp for prevent concurrency Problems
        /// </summary>
        public virtual byte[] RowVersion { get; set; }

        #endregion
    }
}