namespace BasketAPI.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public List<ShopingCartItem> Items { get; set; }

        public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public ShoppingCart()
        {

        }
    }
}
