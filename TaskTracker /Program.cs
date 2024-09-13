using TaskTracker.App.Commands;
using TaskTracker.App.Interfaces;
using TaskTracker.Repository;
using TaskTracker.User_Interface;
using TaskTracker.Utils;

namespace TaskTracker;

internal static class Program
{
    private static void Main(string[] args)
    {
        var fileHelper = new FileHelper(new SystemFileSystem());
        var taskRepository = new JsonTaskRepository(fileHelper);
        var addTaskCommand = new AddTaskCommand(taskRepository);
        var getListsCommand = new GetListsCommand(taskRepository);
        var deleteTaskCommand = new DeleteTaskCommand(taskRepository);
        var updateTaskCommand = new UpdateTaskCommand(taskRepository);
        var updateTaskStatusCommand = new UpdateTaskStatusCommand(taskRepository);

        var command = new Dictionary<string, ICommand>()
        {
            {"list", getListsCommand},
            {"add", addTaskCommand },
            {"delete", deleteTaskCommand },
            {"update", updateTaskCommand },
            {"mark", updateTaskStatusCommand},
        };

        var cli = new CommandLineInterface(command);

        Console.WriteLine("Welcome to Task Tracker!");
        Console.WriteLine("Available commands: add, update, delete, mark-in-progress, mark-done, list");
        Console.WriteLine("Type 'exit' to quit.");

        string? input;
        do
        {
            Console.WriteLine(">");
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input)) continue;
            var commandArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            cli.ProcessCommand(commandArgs);
        } while (input != null && input.ToLower() != "exit");

        Console.WriteLine("Goodbye!");
    }
}