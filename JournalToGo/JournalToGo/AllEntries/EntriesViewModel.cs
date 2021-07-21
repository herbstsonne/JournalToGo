using System;
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
        }
        

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Entries.Clear();
                //var items = await DataStore.GetEntriesAsync(true);
                var items = await _journalContext.JournalEntry.ToListAsync(); 
                items.OrderByDescending(l => Convert.ToDateTime(l.Day));

                foreach (var item in items)
                {
                    Entries.Add(item);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
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