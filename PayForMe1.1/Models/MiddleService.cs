using System;
using System.Collections.Generic;

namespace PayForMe1._1.Models
{
    public partial class MiddleService
    {
        public MiddleService()
        {
            LastServices = new HashSet<LastService>();
            Orders = new HashSet<Order>();
        }

        public int MiddleServiceId { get; set; }
        public string MiddleServiceName { get; set; } = null!;
        public decimal? MiddleServiceCost { get; set; }
        public decimal? MiddleServiceTax { get; set; }
        public int MainServiceId { get; set; }

        public virtual MainService MainService { get; set; } = null!;
        public virtual ICollection<LastService> LastServices { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
