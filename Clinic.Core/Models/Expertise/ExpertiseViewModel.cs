using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Expertise
{
    public class ExpertiseViewModel : BaseViewModel
    {
        #region Public Properties

        public Guid Id { get; set; }

        /// <summary>
        /// تخصص
        /// </summary>
        public string ExpertiseName { get; set; }
        #endregion Public Properties
    }
}