using System;
using JournalToGo.Models;
using Xamarin.Forms;

namespace JournalToGo.ViewModels
{
    public class NewEntryViewModel : BaseViewModel
    {
        private DateTime day = DateTime.Now;
        private string headline;
        private string dailyThoughtsText;

        public NewEntryViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(headline)
                && !String.IsNullOrWhiteSpace(dailyThoughtsText);
        }

        public DateTime Day
        {
            get => day;
            set => SetProperty(ref day, value);
        }

        public string Headline
        {
            get => headline;
            set => SetProperty(ref headline, value);
        }

        public string DailyThoughtsText
        {
            get => dailyThoughtsText;
            set => SetProperty(ref dailyThoughtsText, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            JournalEntry newItem = new JournalEntry()
            {
                Id = Guid.NewGuid().ToString(),
                Day = Day.ToShortDateString(),
                Headline = Headline,
                DailyThoughtsText = DailyThoughtsText
            };

            await DataStore.AddEntryAsync(newItem);

            // This will pop the current page off the navigation stack
            if(Shell.Current == null)
                return;
            await Shell.Current.GoToAsync("..");
        }
    }
}
