using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Clinic.Core.Domains.Common;

namespace Clinic.Core.Domains.Reports
{
    public class Report : BaseEntity
    {
        #region Public Properties

        /// <summary>
        /// </summary>
        [Column(TypeName = "xml")]
        public virtual string Content { get; set; }

        /// <summary>
        /// </summary>
        [NotMapped]
        public virtual XElement XmlContent
        {
            get { return XElement.Parse(Content); }
            set { Content = value.ToString(); }
        }
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }

        #endregion Public Properties

        public virtual ReportParameter ReportParameter { get; set; }
        public virtual Guid? ReportParameterId { get; set; }
    }
}