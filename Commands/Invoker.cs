
namespace jrnl
{
    public class Invoker
    {

        private readonly List<ICommand> _commands = [
            new NewCommand(),
            new ListCommand(),
            new ReadCommand(),
            new DeleteCommand()
        ];

        public void Invoke(string commandName, string[] args)
        {
            if (!IsValidCommand(commandName))
                throw new ArgumentException($"Invalid command: {commandName}");
            else
            {
                ICommand? command = Find(commandName);
                command?.Execute(args);
            }
        }

        private bool IsValidCommand(string commandName)
        {
            if (string.IsNullOrEmpty(commandName)) return false;

            return Find(commandName) is not null;
        }

        private ICommand? Find (string name)
        {
            return _commands.Find(c =>
                String.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
