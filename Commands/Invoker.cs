
namespace jrnl
{
    public class Invoker
    {

        private readonly List<Command> Commands = [
            new NewCommand(),
            new ListCommand(),
            new ReadCommand()
        ];

        public void Invoke(string commandName, string[] args)
        {
            if (!IsValidCommand(commandName))
                throw new ArgumentException($"Invalid command: {commandName}");
            else
            {
                Command? command = Find(commandName);
                command?.Execute(args);
            }
        }

        private bool IsValidCommand(string commandName)
        {
            if (string.IsNullOrEmpty(commandName)) return false;

            return Find(commandName) is not null;
        }

        private Command? Find (string name)
        {
            return Commands.Find(c =>
                String.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
