using FileManager.Commands;
using System;
using System.IO;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Page page = new Page();

            string currentDir;    // Директория, в которой находимся
            try
            {
                currentDir = File.ReadAllText("Directory.json");
            }
            catch (FileNotFoundException)
            {
                currentDir = "C:\\";
            }             

            while (true)
            {
                try
                {
                    page.PrintPage(currentDir);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Отказано в доступе. Будет изменена глубина просмотра файлов.");
                    Console.WriteLine("Нажмите любую клавишу ...");
                    page.ChangeDepth();
                }
                //catch (Exception ex)
                //{
                //    Console.WriteLine($"Ошибка: {ex}");
                //}

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.RightArrow)
                {
                    page.NextPage();
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    page.LastPage();
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    Console.Write("Введите имя каталога:  ");
                    currentDir = Path.Combine(currentDir, Console.ReadLine());
                    page.NewPage(currentDir);
                }
                else if (key == ConsoleKey.Backspace)
                {
                    DirectoryInfo dir = new DirectoryInfo(currentDir);
                    if (dir.Parent != null)
                    {
                        currentDir = dir.Parent.FullName;
                    }
                    page.NewPage(currentDir);
                }
                else if (key == ConsoleKey.Enter)
                {
                    page.ChangeInstr();
                    page.PrintPage(currentDir);
                    Console.Write("Введите команду:  ");

                    string fullUserCommand = Console.ReadLine();
                    string userCommand = "";
                    string userDir = "";
                    try
                    {
                        userCommand = Helper.userCommand(fullUserCommand);
                        userDir = Helper.userDir(fullUserCommand);
                    }
                    catch
                    {
                        
                    }
                    Invoker command = new Invoker();

                    switch(userCommand)
                    {
                        case "cp":
                            command.SetCommand(new Copy(userDir, currentDir));                            
                            break;
                        case "del":
                            command.SetCommand(new Delete(userDir, currentDir));
                            break;
                        case "mv":
                            command.SetCommand(new Move(userDir, currentDir));
                            break;
                        case "inf":
                            command.SetCommand(new Info(userDir, currentDir));
                            break;
                        case "cr dir":
                            command.SetCommand(new CreateDir(userDir, currentDir));
                            break;
                        case "cr file":
                            command.SetCommand(new CreateFile(userDir, currentDir));
                            break;
                        case "find":
                            command.SetCommand(new Find(userDir, currentDir));
                            break;
                        case "sd":
                            command.SetCommand(new StaticData(userDir, currentDir));
                            break;
                        default:
                            command.SetCommand(new Default());
                            break;                        
                    }
                    try
                    {
                        command.Execute();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex}");
                    }
                    Console.WriteLine("\nНажмите любую клавишу ...");
                    page.ChangeInstr();
                    Console.ReadKey(true);
                }
                else if (key == ConsoleKey.Escape)
                {
                    File.WriteAllText("Directory.json", currentDir);
                    break;
                }
            }
        }
    }
}
