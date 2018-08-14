namespace JensCafe.Core.Model
{
    public class CartItem
    {
        public CartItem()
        {
        }

        public MenuItem Item { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}