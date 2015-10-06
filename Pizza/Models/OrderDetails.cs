using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pizza.Models
{
    public class OrderDetails
    {
        public int id { get; set; }
        public int ItemId { get; set; }
        public virtual Order order { get; set; }
        public virtual Item item { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }

        [NotMapped]
        public string itemName
        {
            get
            {
                return item != null ? item.name : "";
            }
        }

        [NotMapped]
        public string sum
        {
            get
            {
                return (quantity * price).ToString("0.00");
            }
        }
    }
}