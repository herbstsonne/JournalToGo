using System;
using JournalToGo.DailyEntries;
using JournalToGo.NewEntries;
using Xamarin.Forms;

namespace JournalToGo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DailyEntryPage), typeof(DailyEntryPage));
            Routing.RegisterRoute(nameof(NewEntryPage), typeof(NewEntryPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
