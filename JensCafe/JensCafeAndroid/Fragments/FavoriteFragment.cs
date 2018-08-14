using Android.OS;
using Android.Views;
using JensCafeAndroid.Adapters;

namespace JensCafeAndroid.Fragments
{
    public class FavoriteFragment : BaseFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();

            HandleEvents();

            menuItems = menuDataService.GetFavoriteItems();
            listView.Adapter = new MenuListAdapter(Activity, menuItems);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.FavoriteFragment, container, false);

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}