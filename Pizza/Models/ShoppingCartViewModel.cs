using System.Collections.Generic;
using Pizza.Models;

namespace Pizza.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}