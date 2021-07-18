using System;
using JournalToGo.Services;
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
            DisplayAlert("Login", "Login successful", "OK");
        }

        private void ToolbarItem_OnClicked(object sender, EventArgs e)
        {
            App.Current.UserAppTheme = 
                App.Current.UserAppTheme == OSAppTheme.Light || App.Current.UserAppTheme == OSAppTheme.Unspecified ? 
                OSAppTheme.Dark : OSAppTheme.Light;
        }
    }
}