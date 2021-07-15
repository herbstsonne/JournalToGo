using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;

namespace JournalToGo.Droid
{
    [BroadcastReceiver(Label = "@string/widget_name")]
    [IntentFilter(new string [] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [IntentFilter(new string[] { "com.companyname.journaltogo.ACTION_WIDGET_NEWENTRY" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/widget_newentry")]
    public class NewEntryWidget : AppWidgetProvider
    {
        //public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        //{
        //    context.StartService(new Intent(context, typeof(UpdateService)));
        //}

        public static string ACTION_WIDGET_NEWENTRY = "Enter new entry";
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            //Update Widget layout
            //Run when create widget or meet update time
            var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(NewEntryWidget)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildRemoteViews(context, appWidgetIds));
        }

        private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            //Build widget layout
            var widgetView = new RemoteViews(context.PackageName, Resource.Layout.widget_newentry);

            //Change text of element on Widget
            SetTextViewText(widgetView);

            //Handle click event of button on Widget
            RegisterClicks(context, appWidgetIds, widgetView);

            return widgetView;
        }

        private void SetTextViewText(RemoteViews widgetView)
        {
            widgetView.SetTextViewText(Resource.Id.blog_title, "HelloAppWidget");
        }

        private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        {
            var intent = new Intent(context, typeof(NewEntryWidget));
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            intent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

            widgetView.SetOnClickPendingIntent(Resource.Id.newentrytext, GetPendingSelfIntent(context, ACTION_WIDGET_NEWENTRY));

        }

        private PendingIntent GetPendingSelfIntent(Context context, string action)
        {
            var intent = new Intent(context, typeof(NewEntryWidget));
            intent.SetAction(action);
            return PendingIntent.GetBroadcast(context, 0, intent, 0);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);

            if (ACTION_WIDGET_NEWENTRY.Equals(intent.Action))
            {
                Toast.MakeText(context, "Show me new entry text", ToastLength.Short).Show();
            }

        }
    }
}