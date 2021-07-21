using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using JournalToGo.NewEntries;

namespace JournalToGo.Droid
{
    [BroadcastReceiver(Label = "@string/widget_name")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [IntentFilter(new string[] { "com.companyname.journaltogo.ACTION_WIDGET_NEWENTRY" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/widget_newentry")]
    public class NewEntryWidget : AppWidgetProvider
    {
        public static string ACTION_WIDGET_NEWENTRYSAVE = "Enter new entry";

        private INewEntryDataEnabler _newEntryDataEnabler = new NewEntryDataEnabler(new JournalingContext());
        
        private RemoteViews widgetView;
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            context.StartService (new Intent (context, typeof (InteractionService)));
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);

            if (ACTION_WIDGET_NEWENTRYSAVE.Equals(intent.Action))
            {
                HandleNewEntry(context);
            }
        }

        private void HandleNewEntry(Context context)
        {
            try
            {
                var entry = JournalEntryFactory.Create(DateTime.Now, "Test widget", "Test");
                _newEntryDataEnabler.Save(entry);
                Toast.MakeText(context, "New entry saved", ToastLength.Short).Show();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Toast.MakeText(context, "New entry could not be saved", ToastLength.Short).Show();
            }
        }
    }
}