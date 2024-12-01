using Microsoft.EntityFrameworkCore;

public class JournalContext : DbContext
{

    public JournalContext ()
    {
        var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
        DbPath = System.IO.Path.Join(path, "jrnl.db");
    }

    DbSet<JournalEntry> JournalEntries { get; set; }

    public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

}

public class JournalEntry
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Body { get; set; }
}