using System;
using System.IO;

namespace FileManager.Commands
{
    public class Find : Command
    {
        public Find(string userDir, string currentDir)
        {
            UserDir = userDir;
            CurrentDir = currentDir;
        }

        public override Result Execute()
        {
            try
            {
                var files = Directory.GetFiles(CurrentDir, UserDir, SearchOption.AllDirectories);
                PrintFiles(files);
                return Result.Ok;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Ошибка доступа.");
                return Result.Exception;
            }
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
