
namespace jrnl
{
    public class ReadCommand : ICommand
    {

        public string Name => "read";

        public void Execute(string[] args)
        {
            var @params = ParseArgs(args);

            JournalEntry? entry = GetEntry(@params.Value, @params.IsInt);

            if (entry is null)
                PrintEntryNotFound();
            else
                PrintEntry(entry);
        }

        private (string Value, bool IsInt) ParseArgs(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException($"'{Name}' requires an argument (id or title)");

            string arg = args[0];

            if (Int32.TryParse(arg, out int i))
                return (arg, true);
            else
                return (arg, false);
        }

        private JournalEntry? GetEntry (string arg, bool isInt)
        {
            JournalEntry? entry = null;
            try
            {
                using var db = new JournalContext();

                if (isInt)
                    entry = db.JournalEntries.Single(je => je.Id == Int32.Parse(arg));
                else
                    entry = db.JournalEntries.Single(je => je.Title == arg);
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

        private void PrintEntryNotFound ()
        {
            Console.WriteLine($"Entry was not found.");
        }

    }
}
