using TaskTracker.App.Interfaces;
using TaskTracker.Repository;

namespace TaskTracker.User_Interface;

public class CommandLineInterface
{
    private readonly Dictionary<string, ICommand> _commands;
    
    public CommandLineInterface(Dictionary<string,ICommand> commands)
    {
        _commands = commands;
    }

    public void ProcessCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Error: No command provided.");
            return;
        }

        string command = args[0].ToLower();
        if (_commands.ContainsKey(command))
        {
            _commands[command].Execute(args.Skip(1).ToArray());
        }
        else
        {
            Console.WriteLine($"Error: Unknown command '{command}'.");
        }
    }
}