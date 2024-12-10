using Microsoft.EntityFrameworkCore;

namespace jrnl 
{
    public class DeleteCommand : ICommand 
    {

        private readonly string[] _options = {"y", "n", "yes", "no"};

        public string Name => "delete";

        public void Execute (string[] args)
        {
            int id = ParseArgs(args);
            JournalEntry? entry = GetEntry(id);
            
            if (entry is null) 
                PrintEntryNotFound(id);
            else 
            {
                PrintEntryFound(entry);
                string response = GetConfirmationFromUser();

                if (response == _options[0] || response == _options[2])  // Confirm (Y)
                {
                    DeleteEntry(id);
                    PrintDeleted();
                }
                else if (response == _options[1] || response == _options[3]) // Cancel (N)
                    PrintCancelled();
            }
        }

        private int ParseArgs (string[] args) 
        {
            if (args.Length < 1) 
                throw new ArgumentException($"Required argument is missing: id");

            int id;
            if(Int32.TryParse(args[0], out id))
                return id;
            else 
                throw new ArgumentException($"Invalid argument: {args[0]}. An integer id is required.");
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

        private void PrintEntryFound (JournalEntry je)
        {
            Console.WriteLine("{0} | {1} | {2} | {3}", je.Id, je.Title, je.Date.ToString("MM/dd/yy"), je.Body);
        }

        private void PrintEntryNotFound (int id)
        {
            Console.WriteLine($"Entry #{id} was not found.");
        }

        private string GetConfirmationFromUser () 
        {
            Console.Write("Delete this entry? (Y/N) ");
            string input = Console.ReadLine() ?? "";

            while (!_options.Contains(input.ToLower())) 
            {
                Console.WriteLine($"Please type {_options[0]} or {_options[1]} and press enter.");
                input = Console.ReadLine() ?? "";
            }
            return input.ToLower();
        }

        private void DeleteEntry (int id) 
        {
            try 
            {
                using var db = new JournalContext();
                db.Remove(db.JournalEntries.Single(je => je.Id == id));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException?.Message);
            }
        }

        private void PrintDeleted () 
        {
            Console.WriteLine("Entry was deleted.");
        }

        private void PrintCancelled ()
        {
            Console.WriteLine("Nothing was deleted.");
        }

    }
}