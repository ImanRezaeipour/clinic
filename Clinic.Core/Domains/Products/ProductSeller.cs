using Clinic.Core.Domains.Common;

namespace Clinic.Core.Domains.Products
{
    /// <summary>  فروشنده محصول </summary>
    /// <remarks>   Iman, 06/04/1396. </remarks>
    public class ProductSeller : BasePerson
    {

        #region Public Properties

        /// <summary>   عنوان شرکت </summary>
        /// <value> The company title. </value>
        public virtual string CompanyTitle { get; set; }

        #endregion Public Properties

    }
}