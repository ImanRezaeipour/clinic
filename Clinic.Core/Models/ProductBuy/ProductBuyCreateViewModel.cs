using System;
using System.Collections.Generic;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.ProductBuy
{
   public class ProductBuyCreateViewModel : BaseViewModel
    {

       //public ProductSellerViewModel Seller { get; set; }
        public  int? BoughtCount { get; set; }

      
        public  DateTime? BoughtOn { get; set; }
       
        public  Guid? ProductId { get; set; }
        public  Guid? SellerId { get; set; }

       
        public  IList<SelectListItem> SellersList { get; set; }

        public  decimal? UnitPrice { get; set; }
    }
}
