namespace Clinic.Core.Domains.Common
{
    /// <summary>
    ///     نشان دهنده موجودیت پایه
    /// </summary>
    public abstract class BaseMessage : BaseEntity
    {
        #region Public Properties

        /// <summary>
        /// </summary>
        public virtual string Body { get; set; }

        /// <summary>
        /// </summary>
        public virtual bool? IsSend { get; set; }

        /// <summary>
        /// </summary>
        public virtual string Title { get; set; }

        #endregion Public Properties
    }
}