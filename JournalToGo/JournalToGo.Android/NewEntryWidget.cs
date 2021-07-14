using Android.App;
using Android.Appwidget;
using Android.Content;

namespace JournalToGo.Droid
{
    [BroadcastReceiver(Label = "@string/widget_name")]
    [IntentFilter(new string [] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/widget_word")]
    public class NewEntryWidget : AppWidgetProvider
    {
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            context.StartService(new Intent(context, typeof(UpdateService)));
        }
    }
}