using System;
using System.Collections.Generic;

namespace PayForMe1._1.Models
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public double Principal { get; set; }
        public double BalanceOfMachine { get; set; }
        public double VodafoneCashWalletBalance { get; set; }
        public double VisaBalance { get; set; }
        public double CashInHand { get; set; }
        public double DebitBalance { get; set; }
        public double Total { get; set; }
        public double DebitsOnMe { get; set; }
        public double NetBalance { get; set; }
        public DateTime InventoryDate { get; set; }
        public string InventoryStatus { get; set; } = null!;
        public bool Growing { get; set; }
        public bool Loss { get; set; }
        public double AmountOfIncrease { get; set; }
        public double LossAmount { get; set; }
    }
}
