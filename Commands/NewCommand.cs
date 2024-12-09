
namespace jrnl
{
    public class NewCommand : Command
    {

        public override string Name => "new";

        public override void Execute(string[] args)
        {
            string title = GetTitleFromUser();

            PrintInstructions();

            string? body = GetBodyFromUser();

            if (!String.IsNullOrEmpty(body))
                SaveEntry(title, body);
            else 
            {
                Console.WriteLine("Blank entry was not saved.");
            }
        }

        private void PrintInstructions ()
        {
            Console.WriteLine("Write a journal entry (Press enter to save):");
        }

        private string GetTitleFromUser()
        {
            Console.Write("Title (Optional): ");
            
            string? title = Console.ReadLine();

            if (String.IsNullOrEmpty(title))
                title = "Untitled";

            return title; 
        }

        private string? GetBodyFromUser () 
        {
            return Console.ReadLine();
        }

        private async void SaveEntry (string title, string body) 
        {
            Console.WriteLine("Saving...");

            try
            {
                using var db = new JournalContext();

                var journalEntry = await db.AddAsync(new JournalEntry
                {
                    Title = title,
                    Body = body,
                });
                db.SaveChanges();
                PrintSuccess(journalEntry.Entity.Title, journalEntry.Entity.Date);
            }
            catch (Exception e)
            {
                PrintFailure(e);
            }
        }

        private void PrintSuccess (string title, DateTime date)
        {
            Console.WriteLine($"New entry saved: {title} | {date.Date.ToString("d")}");
        }

        private void PrintFailure (Exception e) 
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException?.Message);
        }

    }
}
