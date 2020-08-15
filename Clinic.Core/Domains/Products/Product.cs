// file:	Entities\Products\Product.cs
//
// summary:	Implements the product class

using System;
using Clinic.Core.Domains.Common;

namespace Clinic.Core.Domains.Products
{
    /// <summary>   محصول </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class Product : BaseEntity
    {

        #region Public Properties

        /// <summary>  تعداد موجود   </summary>
        /// <value> The count. </value>
        public virtual int? AvailableCount { get; set; }

        /// <summary>   بارکد </summary>
        /// <value> The bar code. </value>
        public virtual string BarCode { get; set; }

        /// <summary>   کد شناسه </summary>
        /// <value> The code. </value>
        public virtual string CustomCode { get; set; }
        /// <summary>   Gets or sets the description. </summary>
        /// <value> The description. </value>
        public virtual string Description { get; set; }

        /// <summary>   آیا محصول قابل فروش است </summary>
        /// <value> True if this object is active, false if not. </value>
        public virtual bool? IsActive { get; set; }

        /// <summary>   آیا محصول است یا دسته </summary>
        /// <value> True if this object is product, false if not. </value>
        public virtual bool? IsProduct { get; set; }

        /// <summary>   دسته والد </summary>
        /// <value> The parent. </value>
        public virtual Product Parent { get; set; }

        /// <summary>   شناسه دسته والد </summary>
        /// <value> The identifier of the parent. </value>
        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// حداکثر تعداد جهت آلارم
        /// </summary>
        public virtual int? AlarmCount { get; set; }


        /// <summary>   عنوان </summary>
        /// <value> The title. </value>
        public virtual string Title { get; set; }

        /// <summary>   قیمت واحد </summary>
        /// <value> The sale price. </value>
        public virtual decimal? UnitPrice { get; set; }

        #endregion Public Properties
    }
}