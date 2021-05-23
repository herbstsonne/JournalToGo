using JournalToGo.ViewModels;
using Xamarin.Forms;

namespace JournalToGo.Views
{
    public partial class NewEntryPage : ContentPage
    {
        public Models.JournalEntry Entry { get; set; }

        public NewEntryPage()
        {
            InitializeComponent();
            BindingContext = new NewEntryViewModel();
        }
    }
}