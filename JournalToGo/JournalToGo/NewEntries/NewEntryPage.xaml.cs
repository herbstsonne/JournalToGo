using Xamarin.Forms;

namespace JournalToGo.NewEntries
{
    public partial class NewEntryPage : ContentPage
    {
        public JournalEntry Entry { get; set; }

        public NewEntryPage()
        {
            InitializeComponent();
            BindingContext = new NewEntryViewModel();
        }
    }
}