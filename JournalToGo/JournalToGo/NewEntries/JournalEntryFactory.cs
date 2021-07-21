using System;

namespace JournalToGo.NewEntries
{
    public static class JournalEntryFactory
    {
        public static JournalEntry Create(DateTime day, string headline, string dailyThoughtsText)
        {
            return new JournalEntry()
            {
                Id = Guid.NewGuid().ToString(),
                Day = day.ToShortDateString(),
                Headline = headline,
                DailyThoughtsText = dailyThoughtsText
            };
        }
    }
}