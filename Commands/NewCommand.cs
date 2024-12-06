
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace jrnl
{
    public class NewCommand : Command
    {

        public override string Name => "new";

        public override void Execute(string[] args)
        {
            string title = GetTitleFromUser();

            PrintInstructions();

            string body = Console.ReadLine() ?? "";

            Console.WriteLine("Saving...");

            using (var db = new JournalContext())
            {
                try
                {
                    EntityEntry<JournalEntry> newEntry = db.Add(new JournalEntry
                    {
                        Title = title,
                        Body = body,
                    });
                    db.SaveChanges();
                    PrintSuccess(newEntry.Entity.Title, newEntry.Entity.Date);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void PrintInstructions ()
        {
            Console.WriteLine("Press enter to finish entry\n" +
                              "Write a journal entry...");
        }

        private string GetTitleFromUser()
        {
            Console.Write("Title (Optional): ");
            return Console.ReadLine() ?? "";
        }

        private void PrintSuccess (string title, DateTime date)
        {
            Console.WriteLine($"New entry saved: {title} | {date.Date.ToString("d")}");
        }

    }
}
