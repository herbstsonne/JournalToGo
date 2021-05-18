using JournalToGo.ViewModels;
using Xamarin.Forms;

namespace JournalToGo.Views
{
    public partial class EntriesPage : ContentPage
    {
        EntriesViewModel _viewModel;

        public EntriesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EntriesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}