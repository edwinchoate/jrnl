
namespace jrnl
{
    class Program
    {

        public static void Main(string[] args)
        {

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

    }
}