using System.Collections.Generic;
using System.Threading.Tasks;

namespace JournalToGo.AllEntries
{
    public interface IAllEntriesDataAccessor
    {
        List<JournalEntry> GetAllEntries(List<JournalEntry> entries);
        JournalEntry GetLatestEntry();
    }
}