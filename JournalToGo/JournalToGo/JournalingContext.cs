using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace JournalToGo
{
    public class JournalingContext : DbContext
    {
        public DbSet<JournalEntry> JournalEntry { get; set; }

        public JournalingContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "journal.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
