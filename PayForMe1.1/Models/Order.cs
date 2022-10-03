using System;
using System.Collections.Generic;

namespace PayForMe1._1.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int? MainServiceId { get; set; }
        public int? MiddleServiceId { get; set; }
        public int? LastServiceId { get; set; }
        public int? PhoneNumber { get; set; }
        public int? LandLineNumber { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
        public bool IsDebt { get; set; }
        public bool? DeductedFromTheBalance { get; set; }
        public decimal Total { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Notes { get; set; }

        public virtual LastService? LastService { get; set; }
        public virtual MainService? MainService { get; set; }
        public virtual MiddleService? MiddleService { get; set; }
        public virtual User? User { get; set; }
    }
}
