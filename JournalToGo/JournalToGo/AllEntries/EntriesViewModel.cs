using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JournalToGo.DailyEntries;
using JournalToGo.NewEntries;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace JournalToGo.AllEntries
{
    public class EntriesViewModel : BaseViewModel
    {
        private readonly AllEntriesDataAccessor _entriesDataAccessor;
        private JournalEntry _selectedItem;

        public ObservableCollection<JournalEntry> Entries { get; }
        public Command LoadEntriesCommand { get; }
        public Command AddEntryCommand { get; }
        public Command<JournalEntry> EntryTapped { get; }

        public EntriesViewModel()
        {
            Title = "All entries";
            Entries = new ObservableCollection<JournalEntry>();
            LoadEntriesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            EntryTapped = new Command<JournalEntry>(this.OnItemSelected);

            AddEntryCommand = new Command(OnAddItem);

            _entriesDataAccessor = new AllEntriesDataAccessor(_journalContext);
        }
        

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Entries.Clear();
                var currentEntries = _entriesDataAccessor.GetAllEntries(new List<JournalEntry>());
                foreach (var entry in currentEntries)
                {
                    Entries.Add(entry);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public JournalEntry SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewEntryPage));
        }

        async void OnItemSelected(JournalEntry item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DailyEntryPage)}?{nameof(DailyEntryViewModel.ItemId)}={item.Id}");
        }

    }
}