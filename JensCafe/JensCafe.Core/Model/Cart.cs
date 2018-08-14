using System.Collections.Generic;

namespace JensCafe.Core.Model
{
	public class Cart
	{
		public Cart()
		{
		}

		public List<CartItem> CartItems
		{
			get;
			set;
		}
	}
}