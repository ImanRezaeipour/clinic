using System;

namespace Clinic.Core.Objects
{
    public class JsTreeObject
    {
        #region Public Properties

        public Guid? Id { get; set; }

        public bool IsSelect { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }

        #endregion Public Properties
    }
}