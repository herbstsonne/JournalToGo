using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace JournalToGo.DailyEntries
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class DailyEntryViewModel : BaseViewModel
    {
        private string itemId;
        private DateTime day;
        private string headline;
        private string dailyThoughtsText;

        private JournalEntry savedEntry;

        public DailyEntryViewModel()
        {
            SaveEntryCommand = new Command(OnSaveEntry, ValidateSave);
            this.PropertyChanged += (_, __) => SaveEntryCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !Equals(Day, savedEntry?.Day) || (!Equals(Headline, savedEntry?.Headline) && !String.IsNullOrWhiteSpace(headline)) 
                || (!Equals(dailyThoughtsText, savedEntry?.DailyThoughtsText)) && !String.IsNullOrWhiteSpace(dailyThoughtsText);
        }

        private async void OnSaveEntry()
        {
            try
            {
                savedEntry.Day = Day;
                savedEntry.Headline = Headline;
                savedEntry.DailyThoughtsText = DailyThoughtsText;
                //await DataStore.UpdateEntryAsync(savedEntry);
                _journalContext.JournalEntry.Update(savedEntry);
                _journalContext.SaveChanges();

                if (Shell.Current == null)
                    return;
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string Id { get; set; }

        public Command SaveEntryCommand { get; }

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

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public void LoadItemId(string itemId)
        {
            try
            {
                //savedEntry = DataStore.GetEntryAsync(ItemId).Result;
                savedEntry = _journalContext.JournalEntry.Find(ItemId);
                Id = savedEntry.Id;
                Day = savedEntry.Day;
                Headline = savedEntry.Headline;
                DailyThoughtsText = savedEntry.DailyThoughtsText;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to load entry");
            }
        }
    }
}
