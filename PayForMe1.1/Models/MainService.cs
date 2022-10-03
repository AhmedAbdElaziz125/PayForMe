using System;
using System.Collections.Generic;

namespace PayForMe1._1.Models
{
    public partial class MainService
    {
        public MainService()
        {
            MiddleServices = new HashSet<MiddleService>();
            Orders = new HashSet<Order>();
        }

        public int MainServiceId { get; set; }
        public string MainServiceName { get; set; } = null!;
        public decimal? MainServiceCost { get; set; }
        public decimal? MainServiceTax { get; set; }

        public virtual ICollection<MiddleService> MiddleServices { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
