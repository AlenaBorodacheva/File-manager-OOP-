using System;

namespace FileManager.Commands
{
    public class Default : ICommand
    {
        public void Execute()
        {
             Console.WriteLine("\nКоманда не найдена.");
        }
    }
}
