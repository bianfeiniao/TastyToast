using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using xam.TastyToast;

namespace App_TastyToast
{
    [Activity(Label = "App_TastyToast", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            SetContentView (Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.showConfusingToast).Click += (a, b) =>
            {
                TastyToast.makeText(this, "I don't Know !", ToastLength.Long,
                     TastyToast.CONFUSING);

            };

            FindViewById<Button>(Resource.Id.showSuccessToast).Click += (a, b) =>
            {
                TastyToast.makeText(this, "Download Successful !", ToastLength.Long,
                     TastyToast.SUCCESS);

            };
            FindViewById<Button>(Resource.Id.showWarningToast).Click += (a, b) =>
            {
                TastyToast.makeText(this, "Are you sure ?", ToastLength.Long,
                   TastyToast.WARNING);

            };
            FindViewById<Button>(Resource.Id.showDefaultToast).Click += (a, b) =>
            {
                TastyToast.makeText(this, "This is Default Toast", ToastLength.Long,
                   TastyToast.DEFAULT);

            };
            FindViewById<Button>(Resource.Id.showInfoToast).Click += (a, b) =>
            {
                TastyToast.makeText(this, "Searching for username : 'Rahul' ", ToastLength.Long,
                   TastyToast.INFO);

            };
            FindViewById<Button>(Resource.Id.showErrorToast).Click += (a, b) =>
            {
                TastyToast.makeText(this, "Downloading failed ! Try again later ", ToastLength.Long,
                 TastyToast.ERROR);

            };






        }
  


    }
}

