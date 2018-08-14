using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using JensCafe.Core.Model;
using JensCafe.Core.Service;
using JensCafeAndroid.Adapters;
using System.Collections.Generic;

namespace JensCafeAndroid
{
    [Activity(Label = "Cart")]
    public class CartActivity : Activity
    {
        private CartDataService cartDataService;
        private List<CartItem> cartItems;
        private ListView cartListView;
        private TextView cartTotalTextView;
        private Button continueOrderingButton;
        private Button checkoutButton;
        private NumberPicker amountNumberPicker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CartView);

            cartDataService = new CartDataService();

            FindViews();

            cartItems = cartDataService.GetCart().CartItems;
            cartListView.Adapter = new CartAdapter(this, cartItems);

            BindData();

            HandleEvents();
        }

        private void FindViews()
        {
            cartListView = FindViewById<ListView>(Resource.Id.cartListView);
            continueOrderingButton = FindViewById<Button>(Resource.Id.continueOrderingButton);
            checkoutButton = FindViewById<Button>(Resource.Id.checkoutButton);
            cartTotalTextView = FindViewById<TextView>(Resource.Id.totalPriceTextView);
        }

        private void HandleEvents()
        {
            continueOrderingButton.Click += ContinueOrderingButton_Click;
            checkoutButton.Click += CheckoutButton_Click;
        }

        private void BindData()
        {
            cartTotalTextView.Text = cartDataService.GetCartTotal().ToString("C2");
        }

        private void ContinueOrderingButton_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MenuActivityWithTabs));
            StartActivity(intent);
        }

        private void CheckoutButton_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(CheckoutActivity));
            intent.PutExtra("total", cartTotalTextView.Text);

            SetResult(Result.Ok, intent);
            StartActivity(intent);
        }
    }
}