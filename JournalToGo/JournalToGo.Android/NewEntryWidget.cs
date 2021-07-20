using System;
using System.Linq;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using JournalToGo.Models;
using JournalToGo.Services;
using Xamarin.Forms.Platform.Android;

namespace JournalToGo.Droid
{
    [BroadcastReceiver(Label = "@string/widget_name")]
    [IntentFilter(new string [] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [IntentFilter(new string[] { "com.companyname.journaltogo.ACTION_WIDGET_NEWENTRY" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/widget_newentry")]
    public class NewEntryWidget : AppWidgetProvider
    {
        public static string ACTION_WIDGET_NEWENTRYSAVE = "Enter new entry";
        private readonly JournalingContext _journalContext = new JournalingContext();
        private RemoteViews widgetView;
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            context.StartService (new Intent (context, typeof (UpdateService)));
            //Update Widget layout
            //Run when create widget or meet update time
            var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(NewEntryWidget)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildRemoteViews(context, appWidgetIds));

        }

        private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            //Build widget layout
            widgetView = new RemoteViews(context.PackageName, Resource.Layout.widget_newentry);

            //Handle click event of button on Widget
            RegisterClicks(context, appWidgetIds, widgetView);

            return widgetView;
        }

        private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        {
            var intent = new Intent(context, typeof(NewEntryWidget));
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            intent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

            widgetView.SetOnClickPendingIntent(Resource.Id.newentrybuttonsave, GetPendingSelfIntent(context, ACTION_WIDGET_NEWENTRYSAVE));

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

            if (intent.Action != null && intent.Action.Equals("APPWIDGET_UPDATE"))
            {
                //Get the newest journal entry of today
                var newestEntry = _journalContext.JournalEntry?.OrderByDescending(e => e.Id).FirstOrDefault(e => e.Day.Equals(DateTime.Now.ToShortDateString()));
                //display the Headline and the date 
                if (newestEntry != null)
                {
                    widgetView.SetTextViewText(Resource.Id.blog_title, newestEntry.Headline);
                }
            }
            else if (ACTION_WIDGET_NEWENTRYSAVE.Equals(intent.Action))
            {
                try
                {
                    SaveNewEntry();
                    Toast.MakeText(context, "New entry saved", ToastLength.Short).Show();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Toast.MakeText(context, "New entry could not be saved", ToastLength.Short).Show();
                }
            }
        }

        private void SaveNewEntry()
        {
            var newItem = new JournalEntry()
            {
                Id = Guid.NewGuid().ToString(),
                Day = DateTime.Now.ToShortDateString(),
                Headline = "Test entry",
                DailyThoughtsText = "Created in widget",
                CreatedByWidget = true
            };
            _journalContext.JournalEntry.Add(newItem);
            _journalContext.SaveChanges();
        }
    }
}