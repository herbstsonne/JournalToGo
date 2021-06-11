using System;
using JournalToGo.Models;
using JournalToGo.Services;
using Xamarin.Forms;

namespace JournalToGo.ViewModels
{
    public class NewEntryViewModel : BaseViewModel
    {
        private DateTime day = DateTime.Now;
        private string headline;
        private string dailyThoughtsText;
        private string searchBook;

        public NewEntryViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            SearchCommand = new Command(OnSearchBook, ValidateSearch);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => SearchCommand.ChangeCanExecute();
        }

        private bool ValidateSearch(object arg)
        {
            return !String.IsNullOrEmpty(SearchBook);
        }

        private void OnSearchBook(object obj)
        {
            var bookService = new GoogleBooksService();
            var book = bookService.GetFirstBook(searchBook);
            SearchBook = book.Id + " " + book.Link;
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

        public string SearchBook
        {
            get => searchBook;
            set => SetProperty(ref searchBook, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command SearchCommand { get; set; }

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

            //await DataStore.AddEntryAsync(newItem);
            _journalContext.JournalEntry.Add(newItem);
            _journalContext.SaveChanges();

            // This will pop the current page off the navigation stack
            if(Shell.Current == null)
                return;
            await Shell.Current.GoToAsync("..");
        }
    }
}
