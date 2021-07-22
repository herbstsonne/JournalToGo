namespace JournalToGo.NewEntries
{
    public interface INewEntryDataAccessor
    {
        bool Validate(string headline, string dailyThoughtsText);
        void Save(JournalEntry entry);
    }
}