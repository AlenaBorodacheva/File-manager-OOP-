using System;
using System.IO;

namespace FileManager.Commands
{
    class Delete : ICommand
    {
        private string userDir;
        private string currentDir;

        public Delete(string userDir, string currentDir)
        {
            this.userDir = userDir;
            this.currentDir = currentDir;
        }

        public void Execute()
        {
            string fullAddress;
            if (userDir.Contains("\\"))
            {
                fullAddress = userDir;
            }
            else
            {
                fullAddress = Path.Combine(currentDir, userDir);
            }
            
            if (File.Exists(fullAddress))       // существует ли файл
            {
                File.Delete(fullAddress);
                Console.WriteLine("\nФайл успешно удален.");
            }
            else if (Directory.Exists(fullAddress))   // тогда существует ли каталог
            {
                DeleteDir(fullAddress);
                Directory.Delete(fullAddress);
                Console.WriteLine("\nКаталог успешно удален.");
            }
            else
            {
                throw new Exception("Такого адреса не существует.");
            }            
        }

        private void DeleteDir(string fullAddress)
        {
            DirectoryInfo dir = new DirectoryInfo(fullAddress);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo f in files)
            {
                f.Delete();
            }
                
            foreach (DirectoryInfo d in dirs)
            {
                DeleteDir(d.FullName);
                if (d.GetDirectories().Length == 0 && d.GetFiles().Length == 0)
                {
                    d.Delete();
                }                    
            }
        }
    }
}
