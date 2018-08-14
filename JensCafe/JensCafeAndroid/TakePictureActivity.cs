using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Widget;
using Java.IO;
using JensCafeAndroid.Utility;
using System;
using System.Threading.Tasks;

namespace JensCafeAndroid
{
    [Activity(Label = "Take a Picture")]
    public class TakePictureActivity : Activity
    {
        private ImageView pictureImageView;
        private Button launchCameraButton;
        private File imageDirectory;
        private File imageFile;
        private Bitmap bitmap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());

            SetContentView(Resource.Layout.TakePictureView);

            FindViews();

            HandleEvents();

            imageDirectory = new File(Android.OS.Environment
                .GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "JensCafeImages");

            if (!imageDirectory.Exists())
            {
                imageDirectory.Mkdirs();
            }
        }

        private void FindViews()
        {
            pictureImageView = FindViewById<ImageView>(Resource.Id.pictureImageView);
            launchCameraButton = FindViewById<Button>(Resource.Id.launchCameraButton);
        }

        private void HandleEvents()
        {
            launchCameraButton.Click += LaunchCameraButton_Click;
        }

        private void LaunchCameraButton_Click(object sender, EventArgs e)
        {
            var permissionCheck = ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.Camera);

            if (permissionCheck == Android.Content.PM.Permission.Granted)
            {
                var intent = new Intent(MediaStore.ActionImageCapture);
                imageFile = new File(imageDirectory, $"JensCafePhoto_{Guid.NewGuid()}.jpg");
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imageFile));
                StartActivityForResult(intent, requestCode: 0);
            }
            else
            {
                SetCameraPermissionAsync();
            }
        }

        public Task SetCameraPermissionAsync()
        {
            return Task.Run(() =>
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, 101);
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 1);
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 1);
            });
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            int width = pictureImageView.Width;
            int height = pictureImageView.Height;
            bitmap = ImageHelper.GetImageBitmapFromFilePath(imageFile.Path, width, height);

            if (bitmap != null)
            {
                pictureImageView.SetImageBitmap(bitmap);
                bitmap = null;
            }

            //required to avoid memory leaks
            GC.Collect();
        }
    }
}