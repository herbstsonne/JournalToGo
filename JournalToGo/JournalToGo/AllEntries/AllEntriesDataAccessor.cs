using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JournalToGo.AllEntries
{
    public class AllEntriesDataAccessor : IAllEntriesDataAccessor
    {
        private readonly JournalingContext _context;

        public AllEntriesDataAccessor(JournalingContext context)
        {
            _context = context;
        }
        public List<JournalEntry> GetAllEntries(List<JournalEntry> entries)
        {
            var items = _context.JournalEntry.ToList(); 
            items.OrderByDescending(l => Convert.ToDateTime(l.Day));

            foreach (var item in items)
            {
                entries.Add(item);
            }

            return entries;
        }

        public JournalEntry GetLatestEntry()
        {
            var allEntries = GetAllEntries(new List<JournalEntry>());
            return allEntries.FirstOrDefault();
        }
    }
}