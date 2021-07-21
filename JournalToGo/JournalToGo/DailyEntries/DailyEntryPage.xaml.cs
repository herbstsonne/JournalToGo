using Xamarin.Forms;

namespace JournalToGo.DailyEntries
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