using System;
using System.IO;
using System.Linq;

namespace FileManager.Commands
{
    public class Copy : Command
    {
        public Copy(string userDir, string currentDir)
        {
            UserDir = userDir;
            CurrentDir = currentDir;
        }

        public override Result Execute()
        {
            string[] addresses = UserDir.Split('>');
            string fullAddress = addresses[0].Contains('\\') ? addresses[0] : Path.Combine(CurrentDir, addresses[0]);
            string dir = addresses[1].Contains('\\') ? addresses[1] : Path.Combine(CurrentDir, addresses[1]);

            string newAddress = dir + "\\" + addresses[0];

            if (File.Exists(fullAddress) && Directory.Exists(dir))
            {
                if (!File.Exists(newAddress))                        // если в этой директории нет файла с таким именем - копируем
                {
                    File.Copy(fullAddress, newAddress);
                }
                else
                {
                    string newAddressRecursive = Helper.GetAddressFile(newAddress);
                    File.Copy(fullAddress, newAddressRecursive);
                }
                Console.WriteLine("Файл успешно скопирован.");
                return Result.Ok;
            }

            if (Directory.Exists(fullAddress) && Directory.Exists(dir))
            {
                if (!Directory.Exists(newAddress))
                {
                    Directory.CreateDirectory(newAddress);
                    CopyRecursive(fullAddress, newAddress);
                }   
                else
                {
                    string newAddressRecursive = Helper.GetAddressDir(newAddress);
                    Directory.CreateDirectory(newAddressRecursive);
                    CopyRecursive(fullAddress, newAddressRecursive);                    
                }
                Console.WriteLine("Каталог успешно скопирован.");
                return Result.Ok;
            }

            Console.WriteLine("Такого адреса не существует.");
            return Result.Exception;
        }
        
        private void CopyRecursive(string addressDir, string newAddress)
        {            
            var dirs = Directory.GetDirectories(addressDir);
            var files = Directory.GetFiles(addressDir);            

            foreach (var d in dirs)
            {
                CopyRecursive(d, newAddress);
                if (Directory.GetDirectories(d).Length == 0)
                {
                    string partAddress = d.Remove(0, addressDir.Length);
                    string address = newAddress + partAddress;
                    Directory.CreateDirectory(address);
                }
            }

            foreach (var f in files)
            {
                string partAddress = f.Remove(0, addressDir.Length);
                string address = newAddress + partAddress;
                File.Copy(f, address);
            }
        }
    }
}
