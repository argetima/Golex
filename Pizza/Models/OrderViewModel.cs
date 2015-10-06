using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza.Models
{
    public class OrderViewModel
    {
        public int id { get; set; }
        public string datetime { get; set; }
        public string phone { get; set; }
        public string adress { get; set; }
        public string sum { get; set; }
    }
}