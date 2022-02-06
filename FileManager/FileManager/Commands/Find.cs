using System;
using System.IO;

namespace FileManager.Commands
{
    public class Find : ICommand
    {
        private string userDir;
        private string currentDir;        

        public Find(string userDir, string currentDir)
        {
            this.userDir = userDir;
            this.currentDir = currentDir;
        }

        public void Execute()
        {
            var files = Directory.GetFiles(currentDir, userDir, SearchOption.AllDirectories);
            PrintFiles(files);
        }

        private void PrintFiles(string[] files)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (files.Length == 0)
            {
                Console.WriteLine("Ничего не найдено.");
            }
            else
            {
                Console.WriteLine("Найденные файлы:");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine(files[i]);
                }
            }
        }
    }
}
