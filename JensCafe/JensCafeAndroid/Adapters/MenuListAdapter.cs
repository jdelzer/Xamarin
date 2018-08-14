using Android.App;
using Android.Views;
using Android.Widget;
using JensCafe.Core.Model;
using JensCafeAndroid.Resources;
using System.Collections.Generic;

namespace JensCafeAndroid.Adapters
{
    public class MenuListAdapter : BaseAdapter<MenuItem>
    {
        private List<MenuItem> items;
        private Activity context;

        public MenuListAdapter(Activity context, List<MenuItem> items)
        {
            this.context = context;
            this.items = items;
        }

        public override MenuItem this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            //Custom row view with image
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.MenuRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.nameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.shortDescriptionTextView).Text = item.ShortDescription;
            convertView.FindViewById<TextView>(Resource.Id.priceTextView).Text = "$ " + item.Price;
            convertView.FindViewById<ImageView>(Resource.Id.menuImageView)
                    .SetImageResource(ResourceHelper.TranslateDrawable(item.ImagePath));

            //Built-in List items with icon
            //if (convertView == null)
            //{
            //    convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
            //}

            //convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;
            //convertView.FindViewById<ImageView>(Android.Resource.Id.Icon)
            //    .SetImageResource(ResourceHelper.TranslateDrawable(item.ImagePath));

            //Built-in Simple list items
            //if (convertView == null)
            //{
            //    convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            //}

            //convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;

            return convertView;
        }
    }
}