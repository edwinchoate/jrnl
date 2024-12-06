using Microsoft.EntityFrameworkCore;

namespace jrnl
{
    public class JournalContext : DbContext
    {

        public JournalContext ()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            DbPath = System.IO.Path.Join(path, "jrnl.db");
        }

        public DbSet<JournalEntry> JournalEntries { get; set; }

        public string DbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JournalEntry>()
                .Property(je => je.Date)
                .HasDefaultValueSql("datetime('now')");
        }

    }

    public class JournalEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
    }
}