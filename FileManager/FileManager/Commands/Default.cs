using System;

namespace FileManager.Commands
{
    public class Default : Command
    {
        public new void Execute()
        {
             Console.WriteLine("\nКоманда не найдена.");
        }
    }
}
