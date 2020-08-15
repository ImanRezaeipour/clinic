using System;
using Clinic.Core.Domains.Common;
using Clinic.Core.Types;

namespace Clinic.Core.Domains.Sales
{
    public class SalePayment:BaseEntity
    {
        /// <summary>   مبلغ باقی مانده </summary>
        /// <value> The remain price. </value>
        public virtual decimal? RemainPrice { get; set; }

        /// <summary>   وضعیت فاکتور </summary>
        /// <value> The payment. </value>
        public virtual PaymentType? Payment { get; set; }

        /// <summary>    پرداخت </summary>
        /// <value> The  payment price. </value>
        public virtual decimal? Amount { get; set; }
        /// <summary>
        /// مرجوعی
        /// </summary>
        public virtual bool? Referral { get; set; }


        public virtual Sale DocumentSale { get; set; }
        public virtual Guid? DocumentSaleId { get; set; }
    }
}
