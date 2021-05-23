using JournalToGo.Services;
using JournalToGo.ViewModels;
using Xamarin.Forms;

namespace JournalToGo.Views
{
    public partial class DailyEntryPage : ContentPage
    {
        public DailyEntryPage()
        {
            InitializeComponent();
            BindingContext = new DailyEntryViewModel();
        }
    }
}