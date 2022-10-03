using System;
using System.Collections.Generic;

namespace PayForMe1._1.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public long? NationalId { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public string? MaritalStatus { get; set; }
        public int? PhoneNumber1 { get; set; }
        public int? PhoneNumber2 { get; set; }
        public int? LandLine { get; set; }
        public decimal? Balance { get; set; }
        public double? Points { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? OrdersCount { get; set; }
        public decimal? Debits { get; set; }
        public string? UserImage { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
