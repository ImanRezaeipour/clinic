using System;
using Clinic.Core.Models.Common;
using Clinic.Core.Types;

namespace Clinic.Core.Models.DocumentSale
{
   public class DocumentSaleEditViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public virtual string Description { get; set; }

        public virtual int DiscountPercent { get; set; }
        public virtual PaymentType Payment { get; set; }

        public virtual decimal PrePaymentPrice { get; set; }

        public virtual decimal RemainPrice { get; set; }

        public virtual decimal TotalFinalPrice { get; set; }

        public virtual decimal TotalOtherPrice { get; set; }

        public virtual int TotalProductCount { get; set; }

        public virtual decimal TotalProductPrice { get; set; }

        public virtual Guid DocumentId { get; set; }

    }
}
