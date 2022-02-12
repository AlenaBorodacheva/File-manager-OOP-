namespace FileManager
{
    public class Invoker
    {
        Command command;

        public void SetCommand(Command com)
        {
            command = com;
        }

        public void Execute()
        {
            command.Execute();
        }
    }
}
