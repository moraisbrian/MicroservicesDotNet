using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
            Items = new List<ShoppingCartItem>();
        }

        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice => Items.Sum(item => item.Quantity * item.Price);
    }
}
