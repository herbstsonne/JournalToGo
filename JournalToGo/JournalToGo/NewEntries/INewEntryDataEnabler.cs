namespace JournalToGo.NewEntries
{
    public interface INewEntryDataEnabler
    {
        bool Validate(string headline, string dailyThoughtsText);
        void Save(JournalEntry entry);
    }
}