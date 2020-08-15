using System;

namespace Clinic.Core.Objects
{
    public class Select2Object
    {
        #region Public Properties

        public string children { get; set; }
        public bool disabled { get; set; }
        public Guid id { get; set; }
        public int level { get; set; }
        public string text { get; set; }
        public int count { get; set; }
        public Guid related_id { get; set; }

        #endregion Public Properties
    }
}