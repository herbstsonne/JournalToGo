using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalToGo.Test.Mocks
{
    public class MockDataStore : IDataStore<JournalEntry>
    {
        readonly List<JournalEntry> items;

        public MockDataStore()
        {
            items = new List<JournalEntry>()
            {
                new JournalEntry { Id = Guid.NewGuid().ToString(), Day = new DateTime(2021, 5, 1).ToShortDateString(), Headline = "Glückstag", DailyThoughtsText="Dies ist mein Glückstag." },
                new JournalEntry { Id = Guid.NewGuid().ToString(), Day = new DateTime(2021, 5, 2).ToShortDateString(), Headline = "Sonnenschein", DailyThoughtsText="This is an item description." },
                new JournalEntry { Id = Guid.NewGuid().ToString(), Day = new DateTime(2021, 5, 3).ToShortDateString(), Headline = "Freunde", DailyThoughtsText="This is an item description." },
                new JournalEntry { Id = Guid.NewGuid().ToString(), Day = new DateTime(2021, 5, 4).ToShortDateString(), Headline = "Relaxed", DailyThoughtsText="This is an item description." },
                new JournalEntry { Id = Guid.NewGuid().ToString(), Day = new DateTime(2021, 5, 5).ToShortDateString(), Headline = "Urlaub", DailyThoughtsText="This is an item description." },
                new JournalEntry { Id = Guid.NewGuid().ToString(), Day = new DateTime(2021, 5, 6).ToShortDateString(), Headline = "Happy", DailyThoughtsText="This is an item description." }
            };
        }

        public async Task<bool> AddEntryAsync(JournalEntry item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateEntryAsync(JournalEntry item)
        {
            var oldItem = items.Where((JournalEntry arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteEntryAsync(string id)
        {
            var oldItem = items.Where((JournalEntry arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<JournalEntry> GetEntryAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<JournalEntry>> GetEntriesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}