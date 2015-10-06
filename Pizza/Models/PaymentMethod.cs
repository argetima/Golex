using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza.Models
{
    public class PaymentMethod
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
    }
}