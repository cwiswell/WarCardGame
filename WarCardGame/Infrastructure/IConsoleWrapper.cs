
namespace WarCardGame.Infrastructure
{
    public interface IConsoleWrapper
    {
        void Write(string line);
        void WriteLine(string line);
        void WriteLine();
        string ReadLine();
    }
}
