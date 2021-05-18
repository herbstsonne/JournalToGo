using JournalToGo.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace JournalToGo.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class DailyEntryViewModel : BaseViewModel
    {
        private string itemId;
        private DateTime day;
        private string headline;
        private string dailyThoughtsText;

        private Models.JournalEntry savedEntry;

        public DailyEntryViewModel()
        {
            savedEntry = DataStore.GetEntryAsync(ItemId).Result;
            SaveEntryCommand = new Command(OnSaveEntry, ValidateSave);
            this.PropertyChanged += (_, __) => SaveEntryCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !Equals(Day, savedEntry.Day) &&
                !String.IsNullOrWhiteSpace(headline) && !Equals(Headline, savedEntry.Headline) &&
                !String.IsNullOrWhiteSpace(dailyThoughtsText) && !Equals(dailyThoughtsText, savedEntry.DailyThoughtsText);
        }

        private async void OnSaveEntry()
        {
            savedEntry.Day = Day;
            savedEntry.Headline = Headline;
            savedEntry.DailyThoughtsText = DailyThoughtsText;
            await DataStore.UpdateEntryAsync(savedEntry);
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

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetEntryAsync(itemId);
                Id = item.Id;
                Day = item.Day;
                Headline = item.Headline;
                DailyThoughtsText = item.DailyThoughtsText;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
