using System;
using JournalToGo.BookSearch;
using Xamarin.Forms;

namespace JournalToGo.NewEntries
{
    public class NewEntryViewModel : BaseViewModel
    {
        private DateTime _day;
        private string _headline;
        private string _dailyThoughtsText;
        private string _searchBook;
        private INewEntryDataAccessor _newEntryhandler;
        private BookSearcher _bookSearcher;

        public DateTime Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }

        public string Headline
        {
            get => _headline;
            set => SetProperty(ref _headline, value);
        }

        public string DailyThoughtsText
        {
            get => _dailyThoughtsText;
            set => SetProperty(ref _dailyThoughtsText, value);
        }

        public string SearchBook
        {
            get => _searchBook;
            set => SetProperty(ref _searchBook, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command SearchCommand { get; set; }
        
        public NewEntryViewModel()
        {
            _newEntryhandler = new NewEntryDataAccessor(_journalContext);
            _bookSearcher = new BookSearcher();
            
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            SearchCommand = new Command(OnSearchBook, ValidateSearch);
            
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => SearchCommand.ChangeCanExecute();
            
            _day = DateTime.Now;
        }

        private bool ValidateSearch(object arg)
        {
            return _bookSearcher.ValidateSearch(SearchBook);
        }

        private void OnSearchBook(object obj)
        {
            SearchBook = _bookSearcher.SearchBook(_searchBook);
        }

        private bool ValidateSave()
        {
            return _newEntryhandler.Validate(_headline, _dailyThoughtsText);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            JournalEntry entry = JournalEntryFactory.Create(Day, Headline, DailyThoughtsText);

            _newEntryhandler.Save(entry);

            // This will pop the current page off the navigation stack
            if(Shell.Current == null)
                return;
            await Shell.Current.GoToAsync("..");
        }
    }
}
