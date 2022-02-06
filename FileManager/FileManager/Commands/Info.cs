using System;
using System.IO;

namespace FileManager.Commands
{
    class Info : ICommand
    {
        private string userDir;
        private string currentDir;

        public Info(string userDir, string currentDir)
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

            FileInfo fileInf = new FileInfo(fullAddress);
            DirectoryInfo dirInf = new DirectoryInfo(fullAddress);
            FileAttributes attributes = File.GetAttributes(fullAddress);

            if (fileInf.Exists)
            {
                MyConsoleColor("\nПолное имя файла: ", fileInf.FullName);
                MyConsoleColor("Дата и время создания: ", $"{fileInf.CreationTime}");
                MyConsoleColor("Дата и время последнего изменения: ", $"{fileInf.LastWriteTime}");
                MyConsoleColor("Размер: ", $"{fileInf.Length} байт");
                MyConsoleColor("Аттрибуты:");

                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    Console.WriteLine("Доступен только для чтения.");
                }                    
                else
                {
                    Console.WriteLine("Доступен для чтения и редактирования.");
                }                    
                if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    Console.WriteLine("Скрытый файл.");
                }                    
                if ((attributes & FileAttributes.System) == FileAttributes.System)
                {
                    Console.WriteLine("Системный файл.");
                }                    
                if ((attributes & FileAttributes.Temporary) == FileAttributes.Temporary)
                {
                    Console.WriteLine("Временный файл.");
                }
            }
            else if (dirInf.Exists)
            {
                MyConsoleColor("\nПолное имя каталога: ", dirInf.FullName);
                MyConsoleColor("Дата и время создания: ", $"{dirInf.CreationTime}");
                MyConsoleColor("Дата и время последнего изменения: ", $"{dirInf.LastWriteTime}");

                double size = 0;
                size = DirSize(fullAddress, ref size);

                if (size != -1)
                {
                    MyConsoleColor("Размер: ", $"{size} байт");
                }                    
                else
                {
                    Console.WriteLine("Размер каталога неизвестен из-за ограничения в доступе.");
                }                    

                MyConsoleColor("Аттрибуты:");

                if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    Console.WriteLine("Скрытый каталог.");
                }                    
                else
                {
                    Console.WriteLine("Доступный каталог.");
                }                    
            }
            else
            {
                throw new Exception("Такого адреса не существует.");
            }
        }

        private double DirSize(string dirName, ref double size)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(dirName);
                DirectoryInfo[] dirs = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo f in files)
                {
                    size += f.Length;
                }

                foreach (DirectoryInfo d in dirs)
                {
                    DirSize(d.FullName, ref size);
                }

                return Math.Round((double)size);
            }
            catch (UnauthorizedAccessException)
            {
                return -1;
            }
        }

        private void MyConsoleColor(string attr, string result = "")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(attr);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(result);
        }
    }
}
