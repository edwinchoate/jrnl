
namespace jrnl
{
    public abstract class Command
    {

        public abstract string Name { get; }

        public abstract void Execute (string[] args);

    }
}
