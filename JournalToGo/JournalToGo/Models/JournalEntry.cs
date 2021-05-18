using System;

namespace JournalToGo.Models
{
    public class JournalEntry
    {
        public string Id { get; set; }
        public string Day { get; set; }
        public string Headline { get; set; }
        public string DailyThoughtsText { get; set; }
    }
}