using Android.App;
using Android.Views;
using Android.Widget;
using JensCafe.Core.Model;
using JensCafeAndroid.Resources;
using System.Collections.Generic;
using System.Globalization;

namespace JensCafeAndroid.Adapters
{
    public class CartAdapter : BaseAdapter<CartItem>
    {
        private List<CartItem> items;
        private Activity context;

        public CartAdapter(Activity context, List<CartItem> items)
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override CartItem this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.CartRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.nameTextView).Text = item.Item.Name;
            convertView.FindViewById<NumberPicker>(Resource.Id.amountEditText).Value = item.Amount;
            convertView.FindViewById<TextView>(Resource.Id.priceTextView).Text = "$" + item.Price.ToString(CultureInfo.CurrentCulture);
            convertView.FindViewById<ImageView>(Resource.Id.itemImageView)
                .SetImageResource(ResourceHelper.TranslateDrawable(item.Item.ImagePath));

            return convertView;
        }
    }
}