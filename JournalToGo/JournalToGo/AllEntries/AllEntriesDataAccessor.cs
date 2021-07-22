using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JournalToGo.AllEntries
{
    public class AllEntriesDataAccessor
    {
        private readonly JournalingContext _context;

        public AllEntriesDataAccessor(JournalingContext context)
        {
            _context = context;
        }
        public async Task<List<JournalEntry>> GetAllEntries(List<JournalEntry> entries)
        {
            entries.Clear();
            //var items = await DataStore.GetEntriesAsync(true);
            var items = await _context.JournalEntry.ToListAsync(); 
            items.OrderByDescending(l => Convert.ToDateTime(l.Day));

            foreach (var item in items)
            {
                entries.Add(item);
            }

            return entries;
        }

        public async Task<JournalEntry> GetLatestEntry()
        {
            var allEntries = await GetAllEntries(new List<JournalEntry>());
            return allEntries.FirstOrDefault();
        }
    }
}