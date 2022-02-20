using System;
using System.IO;

namespace FileManager.Commands
{
    public class Delete : Command
    {
        public Delete(string userDir, string currentDir)
        {
            UserDir = userDir;
            CurrentDir = currentDir;
        }

        public override Result Execute()
        {
            base.Execute();
            try
            {
                if (File.Exists(FullAddress))       // существует ли файл
                {
                    File.Delete(FullAddress);
                    Console.WriteLine("\nФайл успешно удален.");
                }
                else if (Directory.Exists(FullAddress))   // тогда существует ли каталог
                {
                    Directory.Delete(FullAddress, true);
                    Console.WriteLine("\nКаталог успешно удален.");
                }
                else
                {
                    throw new Exception("Такого адреса не существует.");
                }
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
