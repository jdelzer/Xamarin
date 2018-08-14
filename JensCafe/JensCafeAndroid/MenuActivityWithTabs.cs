using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using JensCafe.Core.Model;
using JensCafe.Core.Service;
using JensCafeAndroid.Fragments;
using System.Collections.Generic;

namespace JensCafeAndroid
{
    [Activity(Label = "Menu", Icon = "@mipmap/cafelogo")]
    public class
        MenuActivityWithTabs : Activity
    {
        private ListView menuListView;
        private List<MenuItem> allItems;
        private MenuDataService menuDataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MenuViewWithTabs);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab(" Breakfast", Resource.Mipmap.breakfast, new BreakfastFragment());
            AddTab(" Lunch", Resource.Mipmap.sandwich, new LunchFragment());
            AddTab(" Favorites", Resource.Mipmap.like, new FavoriteFragment());

            //Fill up list of items - Use if not using fragments
            //menuListView = FindViewById<ListView>(Resource.Id.menuListView);

            //menuDataService = new MenuDataService();
            //allItems = menuDataService.GetAllItems();

            //menuListView.Adapter = new MenuListAdapter(this, allItems);

            //menuListView.FastScrollEnabled = true;

            //menuListView.ItemClick += MenuListView_ItemClick;
        }

        private void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(iconResourceId);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            ActionBar.AddTab(tab);
        }

        //private void MenuListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    var item = allItems[e.Position];

        //    var intent = new Intent();
        //    intent.SetClass(this, typeof(MenuDetailActivity));
        //    intent.PutExtra("selectedItemId", item.ItemId);

        //    StartActivityForResult(intent, 100);
        //}

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                var selectedItem = menuDataService.GetItemById(data.GetIntExtra("selectedItemId", 0));

                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Confirmation");
                dialog.SetMessage($"You've added {data.GetIntExtra("amount", 0)} {selectedItem.Name} to your cart.");
                dialog.Show();
            }
        }
    }
}