using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JournalToGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JournalToGo.Droid
{
    public class UpdateService : Service
    {
        public override void OnStart(Intent intent, int startId)
        {
            RemoteViews updateViews = buildUpdate(this);

            ComponentName thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(NewEntryWidget)).Name);
            AppWidgetManager manager = AppWidgetManager.GetInstance(this);
            manager.UpdateAppWidget(thisWidget, updateViews);
        }

        private RemoteViews buildUpdate(Context context)
        {
            var entry = new JournalEntry()
            {
                Day = DateTime.Now.ToShortDateString(),
                Headline = "Hello Widget",
                DailyThoughtsText = "Thoughts"
            };

            var updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget_newentry);

            updateViews.SetTextViewText(Resource.Id.blog_title, entry.Headline);

            return updateViews;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}