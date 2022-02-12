using System;
using System.IO;

namespace FileManager.Commands
{
    public class CreateFile : Command
    {
        public CreateFile(string userDir, string currentDir)
        {
            UserDir = userDir;
            CurrentDir = currentDir;
        }

        public override Result Execute()
        {
            base.Execute();
            try
            {
                File.Create(FullAddress);
                Console.WriteLine("Файл успешно создан.");
                return Result.Ok;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Ошибка доступа.");
                return Result.Exception;
            }
        }
    }
}
