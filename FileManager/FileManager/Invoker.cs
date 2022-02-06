namespace FileManager
{
    public class Invoker
    {
        ICommand command;

        public void SetCommand(ICommand com)
        {
            command = com;
        }

        public void Execute()
        {
            command.Execute();
        }
    }
}
