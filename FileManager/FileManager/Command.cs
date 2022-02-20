using System.IO;
using System.Linq;

namespace FileManager
{
    public class Command
    {
        private protected string UserDir;
        private protected string CurrentDir;
        private protected string FullAddress;

        public virtual Result Execute()
        {
            FullAddress = UserDir.Contains('\\') ? UserDir : Path.Combine(CurrentDir, UserDir);
            return Result.Default;
        }
    }
}
