using System.Collections.Generic;
using System.Threading.Tasks;

namespace JournalToGo.Test.Mocks
{
    public interface IDataStore<T>
    {
        Task<bool> AddEntryAsync(T item);
        Task<bool> UpdateEntryAsync(T item);
        Task<bool> DeleteEntryAsync(string id);
        Task<T> GetEntryAsync(string id);
        Task<IEnumerable<T>> GetEntriesAsync(bool forceRefresh = false);
    }
}
