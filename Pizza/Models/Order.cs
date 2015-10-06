using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Pizza.Models
{
    public class Order
    {
        public int id { get; set; }
        public DateTime datetime { get; set; }
        public string username { get; set; }
        //public AppUser user { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
        
        [DefaultValue(false)]
        public bool ordered { get; set; }
    }
}