using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    class TreeDirectory
    {
        public int depth { private get;  set; }

        public TreeDirectory(int depth)
        {
            this.depth = depth;
        }

        public List<string> GetFiles(string startPath, int level = 0, string start = "")
        {
            if (level == 0)      // для определения заданной глубины вывода каталога
                level = depth;

            List<string> ls = new List<string>();
            string[] folders = Directory.GetDirectories(startPath);

            if (level < depth)
                start = "│   " + start;
            else
                start = "├───" + start;

            for (int i = 0; i < folders.Length; i++)
            {
                DirectoryInfo dirinf = new DirectoryInfo(folders[i]);
                ls.Add(start + dirinf.Name);

                level -= 1;

                if (level > 0)
                    ls.AddRange(GetFiles(folders[i], level, start));
                level += 1;
            }
            string[] files = Directory.GetFiles(startPath);
            foreach (string s in files)
            {
                FileInfo fileinf = new FileInfo(s);
                ls.Add(start + fileinf.Name);
            }
            return ls;
        }
    }
}
