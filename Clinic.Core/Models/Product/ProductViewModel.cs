using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Product
{
   public class ProductViewModel :BaseViewModel
    {
        public Guid Id { get; set; }
        public int AvailableCount { get; set; }


        public string BarCode { get; set; }


        public string CustomCode { get; set; }

        public string Description { get; set; }


        public bool IsActive { get; set; }


        public bool IsProduct { get; set; }

        public Guid ParentId { get; set; }

        public int AlarmCount { get; set; }


        public string Title { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
