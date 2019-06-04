using System;

namespace WarCardGame.Infrastructure
{
    public class ConsoleWrapper : IConsoleWrapper
    {

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void Write(string line)
        {
            Console.Write(line);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
