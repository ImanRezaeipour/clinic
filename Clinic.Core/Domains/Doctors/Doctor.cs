using System;
using Clinic.Core.Domains.Common;

namespace Clinic.Core.Domains.Doctors
{
    /// <summary>   پزشک </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class Doctor : BasePerson
    {

        #region Public Properties

        /// <summary>
        /// تخصص
        /// </summary>
        public virtual Expertise Expertise { get; set; }
        public virtual Guid ExpertiseId { get; set; }

        /// <summary>
        /// کد نظام پزشکی
        /// </summary>
        public virtual string MedicalCode { get; set; }
        public virtual string Shift { get; set; }

        #endregion Public Properties

    }
}