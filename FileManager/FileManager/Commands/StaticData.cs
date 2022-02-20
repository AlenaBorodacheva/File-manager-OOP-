using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.Commands
{
    public class StaticData : Command
    {
        public StaticData(string userDir, string currentDir)
        {
            UserDir = userDir;
            CurrentDir = currentDir;
        }

        public override Result Execute()
        {
            base.Execute();
            try
            {
                string extension = Path.GetExtension(FullAddress);

                if (File.Exists(FullAddress) && extension == ".txt")
                {
                    List<string> text = new List<string>();
                    StreamReader sr = new StreamReader(FullAddress);

                    while (sr.EndOfStream != true)
                    {
                        text.Add(sr.ReadLine());
                    }
                    sr.Close();

                    int wordCount = GetWordCount(text);
                    int lineCount = GetLineCount(text);
                    int paragraphCount = GetParagraphCount(text);
                    int symbolWithSpaceCount = GetSymbolWithSpaceCount(text);
                    int symbolWithoutSpaceCount = GetSymbolWithoutSpaceCount(text);

                    MyConsoleColor("\nКоличество слов: ", wordCount.ToString());
                    MyConsoleColor("Количество строк: ", lineCount.ToString());
                    MyConsoleColor("Количество абзацев: ", paragraphCount.ToString());
                    MyConsoleColor("Количество символов с пробелами: ", symbolWithSpaceCount.ToString());
                    MyConsoleColor("Количество символов без пробелов: ", symbolWithoutSpaceCount.ToString());
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

        private int GetWordCount(List<string> text)
        {
            string s = "";
            foreach(var line in text)
            {
                if(s != "")
                {
                    s = s + " " + line;
                }
                else
                {
                    s += line;
                }                
            }
            string[] words = s.Split(' ');
            return words.Length;
        }

        private int GetLineCount(List<string> text)
        {
            return text.Count;
        }

        private int GetParagraphCount(List<string> text)
        {
            string s = "";
            foreach (var line in text)
            {
                s += line;
            }
            string[] paragraph = s.Split('\n');
            return paragraph.Length;
        }

        private int GetSymbolWithSpaceCount(List<string> text)
        {
            string s = "";
            foreach (var line in text)
            {
                s += line;
            }
            return s.Length;
        }

        private int GetSymbolWithoutSpaceCount(List<string> text)
        {
            string s = "";
            foreach (var line in text)
            {
                s += line;
            }
            s.Trim(new char[] {' ', '\n'});
            return s.Length;
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
