using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Home
{
  public  class ProfileViewModel:BaseViewModel
    {
        public int ProductApprovedCount{get; set;}
        public int ProductPendingCount{get; set;}
        public int ProductRejectCount{get; set;}
        public decimal ReceiptSum { get; set; }
        public Guid UserId { get; set; }

    }
}
