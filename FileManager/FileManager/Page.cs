using System;
using System.Collections.Generic;

namespace FileManager
{
    public class Page
    {
        private readonly char sym = '═';   // Символ рамки
        private readonly int maxHor;       // Размер консоли по горизонтали. Рекомендуется не менее 65.
        private readonly int countStr;          // Кол-во выводимых строк из конфигурационного файла
        private readonly int str;               // кол-во строк дерева
        private int start;             // начальная позиция вывода дерева
        private int finish;            // конечная позиция вывода дерева
        private readonly string instr1;
        private readonly string instr2;
        private string instr;
        private int currentPage;
        private int pages;
        private int depth;

        public Page()
        {           
            Console.BackgroundColor = ConsoleColor.DarkGray;
            maxHor = Config.MaxHor;
            countStr = Config.CountStr;
            depth = Config.Depth;
            str = countStr - 1;            // минус текущая директория
            start = 0;
            finish = str;
            instr1 = Config.instr1;
            instr2 = Config.instr2;
            instr = instr1;
            currentPage = 1;
            if (str != 0)                  // если не выводить весь список целиком                     
            {
                Console.SetWindowSize(maxHor, str + 20);
                Console.SetBufferSize(maxHor, str + 20);
            }
        }

        public void PrintPage(string currentDir)
        {          
            Console.Clear();

            PrintBorder();
            PrintCurrentDir(currentDir);

            TreeDirectory tree = new TreeDirectory(depth);
            List<string> ls = tree.GetFiles(currentDir);
            PrintList(ls);

            if(str != 0)
            {
                PrintCountPages(ls);
            }            
            PrintBorder();

            PrintInstruction();
            PrintBorder();
        }

        public void NextPage()
        {
            if(currentPage < pages)
            {
                currentPage++;
                start = finish;
                finish += str;
            }
        }

        public void LastPage()
        {
            if(currentPage > 1)
            {
                currentPage--;
                finish = start;
                start -= str;
            }            
        }

        public void NewPage(string currentDir)
        {
            start = 0;
            finish = str;
            currentPage = 1;
        }

        public void ChangeInstr()
        {
            instr = instr == instr1 ? instr2 : instr1;
        }

        public void ChangeDepth()
        {
            depth--;
        }

        private void PrintBorder()
        {
            for (int i = 0; i < maxHor - 1; i++)
            {
                Console.Write(sym);
            }
            Console.WriteLine();
        }

        private void PrintCurrentDir(string currentDir)
        {
            Console.ForegroundColor = ConsoleColor.Black; // цвет текущего каталога
            Console.WriteLine(currentDir);
            Console.ForegroundColor = ConsoleColor.White; // цвет всего остального
        }

        private void PrintList(List<string> ls)
        {
            if (finish > ls.Count)
            {
                finish = ls.Count;
            }               

            if (start < 0)
            {
                start = 0;
            }                

            if (finish != 0)
            {
                for (int i = start; i < finish; i++)
                {
                    Console.WriteLine(ls[i]);
                }                    
            }
            else                           //Пользователь захотел просмотреть весь список на одном листе
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    Console.WriteLine(ls[i]);
                }                    
            }
        }

        private void PrintCountPages(List<string> ls)
        {
            if (ls.Count % str != 0)
            {
                pages = ls.Count / str + 1;
            }
            else
            {
                pages = ls.Count / str;
            }
            if(pages == 0)
            {
                pages = 1;
            }

            if (str != 0)
            {
                Console.SetCursorPosition(0, str + 4);
                Console.WriteLine($"\n Страница {currentPage} из {pages}.");
            }
        }

        private void PrintInstruction()
        {
            Console.WriteLine(instr);
        }
    }
}
