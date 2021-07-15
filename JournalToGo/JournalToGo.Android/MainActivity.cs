using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace JournalToGo.Droid
{
    [Activity(Label = "JournalToGo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SQLitePCL.Batteries_V2.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


            //this.SetContentView(Resource.Layout.widget_newentry);
            //EditText edittext = FindViewById<EditText>(Resource.Id.newentrytext);
            //edittext.KeyPress += (object sender, View.KeyEventArgs e) => {
            //    e.Handled = false;
            //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            //    {
            //        Toast.MakeText(this, edittext.Text, ToastLength.Short).Show();
            //        e.Handled = true;
            //    }
            //};
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}