using System;
using System.IO;
using System.Linq;

namespace FileManager.Commands
{
    class Move : ICommand
    {
        private string userDir;
        private string currentDir;

        public Move(string userDir, string currentDir)
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
            }
            else if (Directory.Exists(fullAddress) && Directory.Exists(dir))
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
            }
            else
            {
                throw new Exception("Такого адреса не существует.");
            }
        }
    }
}
