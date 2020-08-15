using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Core.DataAnnotations
{
    /// <summary>
    /// 
    /// </summary>
    public class IsTrueAttribute : ValidationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (value.GetType() != typeof(bool))
                throw new InvalidOperationException("can only be used on boolean properties.");

            return (bool)value;
        }
    }
}
