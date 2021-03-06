﻿using System;
using System.Collections.Generic;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.ProductBuy
{
   public class ProductBuyEditViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public int? BoughtCount { get; set; }

        public DateTime? BoughtOn { get; set; }

        public Guid? ProductId { get; set; }

        public IList<SelectListItem> SellersList { get; set; }

        public decimal? UnitPrice { get; set; }
        public Guid? SellerId { get; set; }
    }
}
