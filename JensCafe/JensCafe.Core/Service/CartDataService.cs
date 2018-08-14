using JensCafe.Core.Model;
using JensCafe.Core.Repository;

namespace JensCafe.Core.Service
{
    public class CartDataService
    {
        private static CartRepository cartRepository = new CartRepository();

        public CartDataService()
        {
        }

        public void AddCartItem(MenuItem menuItem, int amount, double price)
        {
            cartRepository.AddCartItem(menuItem, amount, price);
        }

        public double GetCartTotal()
        {
            return cartRepository.GetCartTotal();
        }

        public Cart GetCart()
        {
            return cartRepository.GetCart();
        }
    }
}