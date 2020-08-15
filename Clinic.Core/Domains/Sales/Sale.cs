// file:	Entities\Documents\DocumentSale.cs
//
// summary:	Implements the document sale class

using System;
using System.Collections.Generic;
using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Documents;

namespace Clinic.Core.Domains.Sales
{
    /// <summary>  فاکتور فروش پرونده </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class Sale : BaseEntity
    {
        #region Public Properties

        /// <summary>  توضیحات  </summary>
        /// <value> The description. </value>
        public virtual string Description { get; set; }

        /// <summary>   تخفیف </summary>
        /// <value> The discount. </value>
        public virtual int DiscountPercent { get; set; }

        /// <summary>   محصولات فروخته شده در فاکتور </summary>
        /// <value> The products. </value>
        public virtual ICollection<SaleProduct> Products { get; set; }

        public virtual ICollection<SalePayment> Payments { get; set; }

      

        /// <summary>   مبلغ نهایی فاکتور برای پرداخت </summary>
        /// <value> The total number of final price. </value>
        public virtual decimal TotalFinalPrice { get; set; }

        /// <summary>   مجموع سایر هزینه ها </summary>
        /// <value> The total number of other price. </value>
        public virtual decimal TotalOtherPrice { get; set; }

        /// <summary>  تعداد کل محصولات موجود در فاکتور  </summary>
        /// <value> The total number of product count. </value>
        public virtual int TotalProductCount { get; set; }

        /// <summary>   مجموع قیمت کل محصولات موجود در فاکتور </summary>
        /// <value> The total number of product price. </value>
        public virtual decimal TotalProductPrice { get; set; }

        /// <summary>   پرونده </summary>
        /// <value> The document. </value>
        public virtual Document Document { get; set; }

        /// <summary>   شناسه پرونده </summary>
        /// <value> The identifier of the document. </value>
        public virtual Guid DocumentId { get; set; }

        #endregion Public Properties
    }
}