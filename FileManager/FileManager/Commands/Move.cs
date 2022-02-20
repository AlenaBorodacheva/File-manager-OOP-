using System;
using System.IO;
using System.Linq;

namespace FileManager.Commands
{
    public class Move : Command
    {
        public Move(string userDir, string currentDir)
        {
            UserDir = userDir;
            CurrentDir = currentDir;
        }

        public override Result Execute()
        {
            try
            {
                string[] addresses = UserDir.Split('>');
                string fullAddress = addresses[0].Contains('\\') ? addresses[0] : Path.Combine(CurrentDir, addresses[0]);
                string dir = addresses[1].Contains('\\') ? addresses[1] : Path.Combine(CurrentDir, addresses[1]);
                string newAddress = dir + "\\" + addresses[0];

                if (File.Exists(fullAddress) && Directory.Exists(dir))
                {
                    if (!File.Exists(newAddress))
                    {
                        File.Move(fullAddress, newAddress);
                    }
                    else
                    {
                        string newAddressRecursive = Helper.GetAddressFile(newAddress);
                        File.Move(fullAddress, newAddressRecursive);
                    }
                    Console.WriteLine("\nФайл успешно перемещен.");
                    return Result.Ok;
                }
                
                if (Directory.Exists(fullAddress) && Directory.Exists(dir))
                {
                    if (!Directory.Exists(newAddress))
                    {
                        Directory.Move(fullAddress, newAddress);
                    }
                    else
                    {
                        string newAddressRecursive = Helper.GetAddressDir(newAddress);
                        Directory.Move(fullAddress, newAddressRecursive);
                    }
                    Console.WriteLine("\nКаталог успешно перемещен.");
                    return Result.Ok;
                }

                Console.WriteLine("Такого адреса не существует.");
                return Result.Exception;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Ошибка доступа.");
                return Result.Exception;
            }
        }
    }
}
