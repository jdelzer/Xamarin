using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace JensCafeAndroid
{
    [Activity(Label = "About Jen's Cafe", Icon = "@mipmap/cafelogo")]
    public class AboutActivity : Activity
    {
        private TextView phoneNumberTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AboutView);

            FindViews();

            HandleEvents();
        }

        private void FindViews()
        {
            phoneNumberTextView = FindViewById<TextView>(Resource.Id.phoneNumberTextView);
        }

        private void HandleEvents()
        {
            phoneNumberTextView.Click += PhoneNumberTextView_Click;
        }

        private void PhoneNumberTextView_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Intent.ActionDial);
            intent.SetData(Android.Net.Uri.FromParts("tel", phoneNumberTextView.Text, null));
            StartActivity(intent);
        }
    }
}