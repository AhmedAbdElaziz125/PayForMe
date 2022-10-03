using System;
using System.Collections.Generic;

namespace PayForMe1._1.Models
{
    public partial class LastService
    {
        public LastService()
        {
            Orders = new HashSet<Order>();
        }

        public int LastServiceId { get; set; }
        public string LastServiceName { get; set; } = null!;
        public decimal? LastServiceCost { get; set; }
        public decimal? LastServiceTax { get; set; }
        public int MiddleServiceId { get; set; }

        public virtual MiddleService MiddleService { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
