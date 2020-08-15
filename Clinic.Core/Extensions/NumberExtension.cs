using System;

namespace Clinic.Core.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class NumberExtension
    {
        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="price"></param>
        /// <param name="previousPrice"></param>
        /// <returns></returns>
        public static decimal? GetDiscount(this decimal? price, decimal? previousPrice)
        {
            if (previousPrice != null)
            {
                decimal? a = Math.Abs(Convert.ToDecimal(previousPrice) - Convert.ToDecimal(price)) /
                             Convert.ToDecimal(previousPrice) * 100;
                string b = a.ToString();
                decimal c = decimal.Parse(b);
                return Math.Floor(Math.Ceiling(Math.Floor(c * 2) / 2));
            }
            return null;
        }

        #endregion Public Methods
    }
}