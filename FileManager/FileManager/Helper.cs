using System.IO;

namespace FileManager
{
    public static class Helper
    {
        public static string userCommand(string fullUserCommand)
        {
            string[] userCommands = fullUserCommand.Split(' ');
            if(userCommands[0] == "cr")
            {
                return userCommands[0] + " " + userCommands[1];
            }
            else
            {
                return userCommands[0];
            }            
        }

        public static string userDir(string fullUserCommand)
        {
            string[] userCommands = fullUserCommand.Split(' ');
            string lastStr = "";

            if (userCommands[0] == "cr")
            {
                for (int i = 2; i < userCommands.Length; i++)
                {
                    lastStr += userCommands[i] + " ";
                }
                return lastStr.Substring(0, lastStr.Length - 1);
            }
            else
            {
                for (int i = 1; i < userCommands.Length; i++)
                {
                    lastStr += userCommands[i] + " ";
                }
                return lastStr.Substring(0, lastStr.Length - 1);
            }
        }
                
        private static void GetAddressFileRecursive(string newAddress, ref int i)
        {            
            string address = Path.ChangeExtension(newAddress, null);
            string extension = Path.GetExtension(newAddress);
            address += $"({i})";
            string result = address + extension;

            if (File.Exists(result))
            {
                i++;
                GetAddressFileRecursive(newAddress, ref i);
            }
        }

        public static string GetAddressFile(string newAddress)
        {
            int i = 1;
            GetAddressFileRecursive(newAddress, ref i);
            string address = Path.ChangeExtension(newAddress, null);
            string extension = Path.GetExtension(newAddress);
            address += $"({i})";
            string result = address + extension;
            return result;
        }

        private static void GetAddressDirRecursive(string newAddress, ref int i)
        {
            newAddress += $"({i})";

            if (Directory.Exists(newAddress))
            {
                i++;
                GetAddressDirRecursive(newAddress, ref i);
            }
        }

        public static string GetAddressDir(string newAddress)
        {
            int i = 1;
            GetAddressDirRecursive(newAddress, ref i);
            newAddress += $"({i})";
            return newAddress;
        }
    }
}
