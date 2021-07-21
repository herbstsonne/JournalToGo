using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace JournalToGo.Droid
{
    [Service]
    public class UpdateService : Service
    {
        public static RemoteViews updateViews;

        public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
        {
            using (RemoteViews updateViews = buildUpdate (this)) {
                // Push update for this widget to the home screen
                ComponentName thisWidget = new ComponentName (this, Java.Lang.Class.FromType (typeof (NewEntryWidget)).Name);
                AppWidgetManager manager = AppWidgetManager.GetInstance (this);
                manager.UpdateAppWidget (thisWidget, updateViews);
            }
            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind (Intent intent)
        {
            return null;
        }

        private RemoteViews buildUpdate (Context context)
        {
            updateViews = new RemoteViews (context.PackageName, Resource.Layout.widget_newentry);

            Intent configIntent = new Intent (context, typeof (MainActivity));
            PendingIntent configPendingIntent = PendingIntent.GetActivity (context, 0, configIntent, 0);
            updateViews.SetOnClickPendingIntent (Resource.Id.blog_title, configPendingIntent);

            return updateViews;
        }
    }
}