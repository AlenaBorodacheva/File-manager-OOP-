using System;
using System.IO;
using System.Linq;

namespace FileManager.Commands
{
    class CreateFile : ICommand
    {
        private string userDir;
        private string currentDir;

        public CreateFile(string userDir, string currentDir)
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
            File.Create(fullAddress);
            Console.WriteLine("Файл успешно создан.");
        }
    }
}
