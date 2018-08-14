using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using JensCafe.Core.Model;
using JensCafe.Core.Service;
using JensCafeAndroid.Resources;
using System;

namespace JensCafeAndroid
{
    [Activity(Label = "Jen's Cafe Menu", Icon = "@mipmap/cafelogo")]
    public class MenuDetailActivity : Activity
    {
        private ImageView itemImageView;
        private TextView itemNameTextView;
        private TextView shortDescriptionTextView;
        private TextView descriptionTextView;
        private TextView priceTextView;
        private EditText amountTextInputEditText;
        private Button cancelButton;
        private Button orderButton;
        private ToggleButton favToggleButton;

        private MenuItem selectedItem;
        private MenuDataService dataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MenuDetailView);
            var selectedItemId = Intent.Extras.GetInt("selectedItemId");

            dataService = new MenuDataService();

            selectedItem = dataService.GetItemById(selectedItemId);

            FindViews();

            BindData();

            HandleEvents();
        }

        private void FindViews()
        {
            itemImageView = FindViewById<ImageView>(Resource.Id.menuImageView);
            itemNameTextView = FindViewById<TextView>(Resource.Id.nameTextView);
            shortDescriptionTextView = FindViewById<TextView>(Resource.Id.shortDescriptionTextView);
            descriptionTextView = FindViewById<TextView>(Resource.Id.descriptionTextView);
            priceTextView = FindViewById<TextView>(Resource.Id.priceTextView);
            amountTextInputEditText = FindViewById<EditText>(Resource.Id.amountEditText);
            cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            orderButton = FindViewById<Button>(Resource.Id.orderButton);
            favToggleButton = FindViewById<ToggleButton>(Resource.Id.favToggleButton);
        }

        private void BindData()
        {
            itemNameTextView.Text = selectedItem.Name;
            shortDescriptionTextView.Text = selectedItem.ShortDescription;
            descriptionTextView.Text = selectedItem.Description;
            priceTextView.Text = "Price: $" + selectedItem.Price;
            favToggleButton.Checked = selectedItem.IsFavorite;

            if (favToggleButton.Checked)
                favToggleButton.SetBackgroundColor(Color.LimeGreen);

            itemImageView.SetImageResource(ResourceHelper.TranslateDrawable(selectedItem.ImagePath));
        }

        private void HandleEvents()
        {
            orderButton.Click += OrderButton_Click;

            cancelButton.Click += (sender, e) =>
            {
                SetResult(Result.Canceled);

                Finish();
            };

            favToggleButton.Click += FavToggleButton_Click;
            amountTextInputEditText.Click += AmountTextInputEditText_Click;
        }

        private void AmountTextInputEditText_Click(object sender, EventArgs e)
        {
            amountTextInputEditText.SetSelection(0, 1);
        }

        private void FavToggleButton_Click(object sender, EventArgs e)
        {
            if (favToggleButton.Checked)
            {
                selectedItem.IsFavorite = true;
                favToggleButton.SetBackgroundColor(Color.LimeGreen);
            }
            else
            {
                selectedItem.IsFavorite = false;
                favToggleButton.SetBackgroundColor(Color.Black);
            }
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            var amount = Int32.Parse(amountTextInputEditText.Text);
            var price = selectedItem.Price;
            AddToCart(selectedItem, amount, price);

            var intent = new Intent();
            intent.PutExtra("selectedItemId", selectedItem.ItemId);
            intent.PutExtra("amount", amount);

            SetResult(Result.Ok, intent);

            ShowDialog(intent);
        }

        private void ShowDialog(Intent intent)
        {
            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Confirmation");
            dialog.SetMessage($"You've added {intent.GetIntExtra("amount", 0)} {selectedItem.Name} to your cart.");
            dialog.SetPositiveButton("View Cart", (c, ev) => { DisplayCart(); });
            dialog.SetNegativeButton("Order More Items", (c, ev) => { DisplayMenu(); });
            dialog.Create().Show();

            //Display dialog
            //var dialog = new AlertDialog.Builder(this);
            //dialog.SetTitle("Confirmation");
            //dialog.SetMessage("Your menu item has been added to your cart");
            //dialog.Show();
        }

        private void DisplayCart()
        {
            Intent intent = new Intent(this, typeof(CartActivity));
            StartActivity(intent);
        }

        private void DisplayMenu()
        {
            Intent intent = new Intent(this, typeof(MenuActivityWithTabs));
            StartActivity(intent);
        }

        public void AddToCart(MenuItem item, int amount, double price)
        {
            CartDataService cartDataService = new CartDataService();
            cartDataService.AddCartItem(item, amount, price);
        }
    }
}