using System;
using Clinic.Core.Domains.Common;

namespace Clinic.Core.Domains.Documents
{
    /// <summary>  عکس های پرونده </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class DocumentImage : BaseEntity
    {

        #region Public Properties

        /// <summary>   پرونده </summary>
        /// <value> The document. </value>
        public virtual Document Document { get; set; }

        /// <summary>  شناسه پرونده </summary>
        /// <value> The identifier of the document. </value>
        public virtual Guid DocumentId { get; set; }

        /// <summary>
        ///     حجم عکس
        /// </summary>
        public virtual string FileDimension { get; set; }

        /// <summary>
        ///     نام فایل
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        ///     سایز عکس
        /// </summary>
        public virtual string FileSize { get; set; }

        /// <summary>
        ///     عنوان عکس
        /// </summary>
        public virtual string Title { get; set; }

        #endregion Public Properties

    }
}