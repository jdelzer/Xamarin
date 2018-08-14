using Android.App;
using Android.OS;
using Android.Widget;

namespace JensCafeAndroid
{
    [Activity(Label = "PaymentActivity")]
    public class PaymentActivity : Activity
    {
        private ImageView creditCardImageView;
        private CheckedTextView useBillingAddressCheckedTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PaymentView);

            FindViews();

            BindData();
        }

        private void BindData()
        {
        }

        private void FindViews()
        {
            creditCardImageView = FindViewById<ImageView>(Resource.Id.creditCardImageView);
            useBillingAddressCheckedTextView =
                FindViewById<CheckedTextView>(Resource.Id.useBillingAddressCheckedTextView);
        }
    }
}