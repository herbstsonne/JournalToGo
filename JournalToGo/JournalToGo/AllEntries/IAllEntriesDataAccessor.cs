using System.Collections.Generic;
using System.Threading.Tasks;

namespace JournalToGo.AllEntries
{
    public interface IAllEntriesDataAccessor
    {
        Task<List<JournalEntry>> GetAllEntries(List<JournalEntry> entries);
        Task<JournalEntry> GetLatestEntry();
    }
}