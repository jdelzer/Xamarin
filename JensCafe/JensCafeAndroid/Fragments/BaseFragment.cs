using Android.App;
using Android.Content;
using Android.Widget;
using JensCafe.Core.Model;
using JensCafe.Core.Service;
using System.Collections.Generic;

namespace JensCafeAndroid.Fragments
{
    public class BaseFragment : Fragment
    {
        protected ListView listView;
        protected MenuDataService menuDataService;
        protected List<MenuItem> menuItems;

        public BaseFragment()
        {
            menuDataService = new MenuDataService();
        }

        public void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
        }

        public void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.menuListView);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = menuItems[e.Position];

            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(MenuDetailActivity));
            intent.PutExtra("selectedItemId", item.ItemId);

            StartActivityForResult(intent, 100);
        }
    }
}