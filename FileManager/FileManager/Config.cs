using System.Configuration;

namespace FileManager
{
    public static class Config
    {
        public static int Depth
        {
            get 
            {
                string _depth = ConfigurationManager.AppSettings["depth"]; // глубина вывода каталога
                if (int.TryParse(_depth, out int depth) && depth > 0)
                {
                    return depth;
                }    
                return 2;     
            }
        }


        public static int CountStr
        {
            get
            {
                string _countStr = ConfigurationManager.AppSettings["str_count"]; // количество строк на одной странице
                if (int.TryParse(_countStr, out int countStr) && countStr > 0)
                {
                    return countStr;
                }   
                return 15;      
            }
        }


        public static int MaxHor
        {
            get
            {
                string _maxHor = ConfigurationManager.AppSettings["max_horizontal"];  // максимальный горизонтальный размер окна
                if (int.TryParse(_maxHor, out int maxHor))
                {
                    if(maxHor < 65)
                    {
                        return 65;
                    }    
                    return maxHor;
                }    
                return 150;  
            }
        }


        public readonly static string instr1 = " Используйте стрелки вправо и влево для перемещения по листам." +
            "\n Для перехода в следующий каталог нажмите пробел." +
            "\n Для возвращения в предыдущий каталог нажмите Backspace." +
            "\n Для ввода команды нажмите Enter," +
            "\n Для выхода из программы нажмите Esc.";

        public readonly static string instr2 = " inf - получение информации о файле или каталоге," +
            "\n del - удаление файла или каталога," +
            "\n cp - копирование файла или каталога," +
            "\n mv - перемещение файла или каталога (с перезаписью)," +
            "\n cr dir - создание каталога," +
            "\n cr file - создание файла," +
            "\n find - поиск по маске (используйте ? для пропуска одного символа и * для пропуска нескольких символов)," +
            "\n sd - получение статических данных для текстовых файлов.";
    }
}
