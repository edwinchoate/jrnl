
namespace jrnl
{
    public class ReadCommand : ICommand
    {

        public string Name => "read";

        public void Execute(string[] args)
        {
            var @params = ParseArgs(args); 

            JournalEntry? entry;

            if (@params.IsInt)
                entry = GetEntry(Int32.Parse(@params.Value));
            else
                entry = GetEntry(@params.Value);

            if (entry is null)
                PrintEntryNotFound(args[0]);
            else
                PrintEntry(entry);
        }

        private (string Value, bool IsInt) ParseArgs(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException($"'{Name}' requires an argument (id or title)");

            int id;
            if (Int32.TryParse(args[0], out id))
                return (args[0], true);
            else
                return (args[0], false);
        }

        private JournalEntry? GetEntry (int id)
        {
            JournalEntry? entry = null;
            try
            {
                using var db = new JournalContext();
                entry = db.JournalEntries.Single(je => je.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return entry;
        }

        private JournalEntry? GetEntry (string title)
        {
            JournalEntry? entry = null;
            try
            {
                using var db = new JournalContext();
                entry = db.JournalEntries.Single(je => je.Title == title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return entry;
        }

        private void PrintEntry (JournalEntry entry) 
        {
            Console.WriteLine(entry.Title);
            Console.WriteLine(entry.Date.ToString("f"));
            Console.WriteLine(entry.Body);
        }

        private void PrintEntryNotFound (string arg)
        {
            Console.WriteLine($"Entry was not found.");
        }

    }
}
