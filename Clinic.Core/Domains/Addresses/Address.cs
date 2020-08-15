using System.Collections.Generic;
using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Doctors;
using Clinic.Core.Domains.Presenters;

namespace Clinic.Core.Domains.Addresses
{
   public class Address :BaseEntity
    {
        #region Properties

        /// <summary>
        ///     طول جغرافیایی
        /// </summary>
        public virtual string Latitude { get; set; }

        /// <summary>
        ///     عرض جغرافیای
        /// </summary>
        public virtual string Longitude { get; set; }


        /// <summary>
        /// </summary>
        public virtual string Street { get; set; }

        /// <summary>
        /// </summary>
        public virtual string Extra { get; set; }

        /// <summary>
        ///     کد پستی شرکت
        /// </summary>
        public virtual string PostalCode { get; set; }

        #endregion

        #region NavigationProperties

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Presenter> Presenters { get; set; }



        #endregion
    }
}
