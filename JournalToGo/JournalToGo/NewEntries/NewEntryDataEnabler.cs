using System;

namespace JournalToGo.NewEntries
{
    public class NewEntryDataEnabler : INewEntryDataEnabler
    {
        private readonly JournalingContext _context;

        public NewEntryDataEnabler(JournalingContext context)
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