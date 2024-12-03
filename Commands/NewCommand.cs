
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

            using (var db = new JournalContext())
            {
                db.Add(new JournalEntry { 
                    Title = title,
                    Body = body,
                });
                db.SaveChanges();
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

    }
}
