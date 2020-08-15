// file:	entities\documents\documentsaleproduct.cs
//
// summary:	Implements the documentsaleproduct class

using System;
using Clinic.Core.Domains.Common;
using Clinic.Core.Domains.Products;

namespace Clinic.Core.Domains.Sales
{
    /// <summary>   لیست محصولات فروخته شده در فاکتور فروش </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class SaleProduct : BaseEntity
    {

        #region Public Properties

        /// <summary>   توضیحات </summary>
        /// <value> The description. </value>
        public virtual string Description { get; set; }

        /// <summary>   آیا محصول مرجوع شده است </summary>
        /// <value> True if this object is return, false if not. </value>
        public virtual bool? IsReturn { get; set; }

        /// <summary>   محصول </summary>
        /// <value> The product. </value>
        public virtual Product Product { get; set; }

        /// <summary>   کد محصول </summary>
        /// <value> The product code. </value>
        public virtual string ProductCode { get; set; }

        /// <summary>   تعداد محصول  </summary>
        /// <value> The count. </value>
        public virtual int? ProductCount { get; set; }
        /// <summary>   شناسه محصول </summary>
        /// <value> The identifier of the product. </value>
        public virtual Guid? ProductId { get; set; }

        /// <summary>   قیمت واحد محصول </summary>
        /// <value> The product price. </value>
        public virtual decimal? ProductPrice { get; set; }

        /// <summary>   عنوان محصول </summary>
        /// <value> The product title. </value>
        public virtual string ProductTitle { get; set; }

        /// <summary>   مجموع قیمت </summary>
        /// <value> The total number of price. </value>
        public virtual decimal? TotalPrice { get; set; }

        #endregion Public Properties

    }
}