using System;
using Clinic.Core.Domains.Common;

namespace Clinic.Core.Domains.Patients
{
    /// <summary>   بیمار </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class Patient : BasePerson
    {

        #region Public Properties

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public virtual DateTime? BirthDayOn { get; set; }

        /// <summary>   محل تولد </summary>
        /// <value> The birth day place. </value>
        public virtual string BirthDayPlace { get; set; }

        /// <summary>   شماره شناسنامه </summary>
        /// <value> The certificate code. </value>
        public virtual string CertificateCode { get; set; }

        

        /// <summary>
        /// شماره پرونده
        /// </summary>
        public virtual string CustomeCode { get; set; }

        /// <summary>
        /// نام پدر
        /// </summary>
        public virtual string FatherName { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public virtual string NationalCode { get; set; }

        #endregion Public Properties

    }
}