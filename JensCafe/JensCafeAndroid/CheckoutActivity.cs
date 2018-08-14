using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using JensCafe.Core.Service;
using JensCafeAndroid.Resources;

namespace JensCafeAndroid
{
    [Activity(Label = "Checkout")]
    public class CheckoutActivity : Activity
    {
        private ImageView creditCardImageView;
        private EditText firsNameEditText;
        private EditText lastNameEditText;
        private EditText addressEditText;
        private AutoCompleteTextView cityTextView;
        private EditText zipCodeEditText;
        private TextView subTotalTextView;
        private CartDataService dataService;
        private TextView taxTextView;
        private TextView totalTextView;
        private Button goToPaymentButton;

        private double subTotal;
        private double tax;

        private const double SALES_TAX_RATE = 0.0826;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CheckoutView);

            dataService = new CartDataService();

            FindViews();

            BindData();

            HandleEvents();

            Spinner stateSpinner = FindViewById<Spinner>(Resource.Id.stateSpinner);
            var adapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, ResourceHelper.StateList);

            stateSpinner.Adapter = adapter;
        }

        private void FindViews()
        {
            creditCardImageView = FindViewById<ImageView>(Resource.Id.creditCardImageView);
            firsNameEditText = FindViewById<EditText>(Resource.Id.firstNameEditText);
            lastNameEditText = FindViewById<EditText>(Resource.Id.lastNameEditText);
            addressEditText = FindViewById<EditText>(Resource.Id.addressEditText);
            cityTextView = FindViewById<AutoCompleteTextView>(Resource.Id.cityAutoCompleteTextView);
            zipCodeEditText = FindViewById<EditText>(Resource.Id.zipCodeEditText);
            subTotalTextView = FindViewById<TextView>(Resource.Id.orderSubtotalTextView);
            taxTextView = FindViewById<TextView>(Resource.Id.orderTaxTextView);
            totalTextView = FindViewById<TextView>(Resource.Id.orderTotalTextView);
            goToPaymentButton = FindViewById<Button>(Resource.Id.goToPaymentButton);
        }

        private void BindData()
        {
            subTotal = dataService.GetCartTotal();
            taxTextView.Text = CalculateTax().ToString("C2");
            subTotalTextView.Text = subTotal.ToString("C2");
            totalTextView.Text = "$" + CalculateTotal().ToString("f2");
        }

        private double CalculateTax()
        {
            tax = subTotal * SALES_TAX_RATE;
            return tax;
        }

        private double CalculateTotal()
        {
            return subTotal + tax;
        }

        private void HandleEvents()
        {
            goToPaymentButton.Click += GoToPaymentButton_Click;
        }

        private void GoToPaymentButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(PaymentActivity));
            StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}