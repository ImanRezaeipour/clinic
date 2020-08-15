using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.DocumentSaleProduct
{
   public class DocumentSaleProductEditViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public virtual string Description { get; set; }

        public virtual bool? IsReturn { get; set; }

        public virtual string ProductCode { get; set; }

        public virtual int? ProductCount { get; set; }
        public virtual Guid? ProductId { get; set; }

        public virtual decimal? ProductPrice { get; set; }

        public virtual string ProductTitle { get; set; }

        public virtual decimal? TotalPrice { get; set; }

    }
}
