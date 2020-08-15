using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Expertise
{
    public class ExpertiseEditViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// تخصص
        /// </summary>
        public string ExpertiseName { get; set; }
        public Guid Id { get; set; }

       

        #endregion Public Properties
    }
}