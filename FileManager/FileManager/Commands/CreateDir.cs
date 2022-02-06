using System;
using System.IO;
using System.Linq;

namespace FileManager.Commands
{
    public class CreateDir : ICommand
    {
        private string userDir;
        private string currentDir;

        public CreateDir(string userDir, string currentDir)
        {
            this.userDir = userDir;
            this.currentDir = currentDir;
        }

        public void Execute()
        {
            string fullAddress;
            if (userDir.Contains('\\'))
            {
                fullAddress = userDir;
            }
            else
            {
                fullAddress = Path.Combine(currentDir, userDir);
            }
            Directory.CreateDirectory(fullAddress);
            Console.WriteLine("Каталог успешно создан.");
        }
    }
}
