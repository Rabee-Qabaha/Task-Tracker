using TaskTracker.Repository;
using TaskTracker.User_Interface;

namespace TaskTracker;

internal static class Program
{
    private static void Main(string[] args)
    {
        var taskRepository = new JsonTaskRepository();
        var cli = new CommandLineInterface(taskRepository);

        Console.WriteLine("Welcome to Task Tracker!");
        Console.WriteLine("Available commands: add, update, delete, mark-in-progress, mark-done, list");
        Console.WriteLine("Type 'exit' to quit.");

        string? input;
        do
        {
            Console.Write("> "); // Shows a prompt for better user experience
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input)) continue;
            var commandArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            cli.ProcessCommand(commandArgs);
        } while (input != null && input.ToLower() != "exit");

        Console.WriteLine("Goodbye!");
    }
}