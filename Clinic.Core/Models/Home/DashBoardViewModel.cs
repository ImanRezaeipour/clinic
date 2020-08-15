using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.Home
{
  public  class DashBoardViewModel:BaseViewModel
    {
        public int ProductApprovedCount{get; set;}
        public int ProductPendingCount{get; set;}
        public int ProductRejectCount{get; set;}
        public decimal ReceiptSum { get; set; }
        public Guid UserId { get; set; }
        public int AllVisitCount  { get; set; }
        public int AllProductCount  { get; set; }
        public int AllCompanyCount  { get; set; }
        public int AllCategoryCount  { get; set; }
        public int AllUserCount  { get; set; }


    }
}
