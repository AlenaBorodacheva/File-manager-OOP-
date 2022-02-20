using System;
using System.IO;

namespace FileManager.Commands
{
    public class CreateDir : Command
    {
        public CreateDir(string userDir, string currentDir)
        {
            UserDir = userDir;
            CurrentDir = currentDir;
        }

        public override Result Execute()
        {
            base.Execute();
            try
            {
                Directory.CreateDirectory(FullAddress);
                Console.WriteLine("Каталог успешно создан.");
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
