using System;
using System.Collections.Generic;
using Clinic.Core.Models.Common;
using SelectListItem = Clinic.Core.Models.Common.SelectListItem;

namespace Clinic.Core.Models.Address
{
    /// <summary>
    /// 
    /// </summary>
    public class AddressEditViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}