using JournalToGo.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Xamarin.Essentials;

namespace JournalToGo.Services
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
