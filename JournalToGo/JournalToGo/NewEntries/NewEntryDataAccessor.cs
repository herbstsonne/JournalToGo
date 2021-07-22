using System;

namespace JournalToGo.NewEntries
{
    public class NewEntryDataAccessor : INewEntryDataAccessor
    {
        private readonly JournalingContext _context;

        public NewEntryDataAccessor(JournalingContext context)
        {
            _context = context;
        }

        public bool Validate(string headline, string dailyThoughtsText)
        {
            return !String.IsNullOrWhiteSpace(headline)
                   && !String.IsNullOrWhiteSpace(dailyThoughtsText);
        }
        
        public void Save(JournalEntry entry)
        {
            _context.JournalEntry.Add(entry);
            _context.SaveChanges();
        }
    }
}