
namespace jrnl
{
    class Program
    {

        const string version = "0.0.0";

        public static void Main(string[] args)
        {

            if (args.Contains("--help"))
            {
                PrintHelp();
                return;
            }

            Invoker invoker = new();

            using (var db = new JournalContext()) 
            {
                db.Database.EnsureCreated();
            }

            try
            {
                if (args.Length == 0)
                    invoker.Invoke(new NewCommand().Name, []);
                else
                {
                    invoker.Invoke(args[0], args.Skip(1).Take(args.Length-1).ToArray());
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static void PrintHelp () 
        {
            Console.WriteLine("jrnl");
            Console.WriteLine($"v{version}");
            Console.WriteLine();
            Console.WriteLine(
                @"Usage
    new               - Add a new entry
    list              - View list of all entries
    read [title|id]   - View full text of an entry
    delete [id]       - Delete a specific entry"
            );
        }

    }
}