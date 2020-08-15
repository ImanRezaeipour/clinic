using System;
using Clinic.Core.Domains.Common;

namespace Clinic.Core.Domains.Products
{
    /// <summary>  خرید محصول </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class ProductBuy : BaseEntity
    {

        #region Public Properties

        /// <summary>   تعداد خریداری شده </summary>
        /// <value> The count. </value>
        public virtual int? BoughtCount { get; set; }

        /// <summary>  زمان خرید محصول  </summary>
        /// <value> The bought on. </value>
        public virtual DateTime? BoughtOn { get; set; }
        /// <summary>  محصول </summary>
        /// <value> The product. </value>
        public virtual Product Product { get; set; }

        /// <summary>  شناسه محصول </summary>
        /// <value> The identifier of the product. </value>
        public virtual Guid? ProductId { get; set; }

        /// <summary>   فروشنده </summary>
        /// <value> The seller. </value>
        public virtual ProductSeller Seller { get; set; }

        /// <summary>   شناسه فروشنده </summary>
        /// <value> The identifier of the seller. </value>
        public virtual Guid? SellerId { get; set; }

        /// <summary>  قیمت واحد </summary>
        /// <value> The unit price. </value>
        public virtual decimal? UnitPrice { get; set; }

        #endregion Public Properties

    }
}