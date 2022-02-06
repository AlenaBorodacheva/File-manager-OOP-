using System;
using System.IO;
using System.Linq;

namespace FileManager.Commands
{
    public class Copy : ICommand
    {
        private string userDir;
        private string currentDir;

        public Copy(string userDir, string currentDir)
        {
            this.userDir = userDir;
            this.currentDir = currentDir;
        }

        public void Execute()
        {
            string[] addresses = userDir.Split('>');

            string fullAddress;
            if (addresses[0].Contains('\\'))
            {
                fullAddress = addresses[0];
            }
            else
            {
                fullAddress = Path.Combine(currentDir, addresses[0]);
            }

            string dir;
            if (addresses[1].Contains('\\'))
            {
                dir = addresses[1];
            }
            else
            {
                dir = Path.Combine(currentDir, addresses[1]);
            }
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
            }
            else if (Directory.Exists(fullAddress) && Directory.Exists(dir))
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
            }
            else
            {
                throw new Exception("Такого адреса не существует.");
            }                
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
