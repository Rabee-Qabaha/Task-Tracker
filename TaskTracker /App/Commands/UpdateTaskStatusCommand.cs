using TaskTracker.App.Interfaces;
using TaskTracker.Domain;
using TaskTracker.Exceptions;
using TaskTracker.Repository;

namespace TaskTracker.App.Commands;

public class UpdateTaskStatusCommand : ICommand
{
    private readonly JsonTaskRepository _repository;

    public UpdateTaskStatusCommand(JsonTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Error: Both ID and Status must be provided.");
            return;
        }

        if (string.IsNullOrWhiteSpace(args[0]))
        {
            Console.WriteLine("Error: The status must not be empty.");
            return;
        }

        if (!int.TryParse(args[1], out var taskId))
        {
            Console.WriteLine("Error: The first argument must be a valid numeric ID.");
            return;
        }

        try
        {
            switch (args[0].ToLower())
            {
                case "done":
                    await _repository.UpdateTaskStatusAsync(taskId, StatusTask.Done);
                    break;
                case "in-progress":
                    await _repository.UpdateTaskStatusAsync(taskId, StatusTask.InProgress);
                    break;
                case "todo":
                    await _repository.UpdateTaskStatusAsync(taskId, StatusTask.Todo);
                    break;
                default:
                    Console.WriteLine($"Error: Unknown status '{args[0]}'.");
                    break;
            }
        }
        catch (TaskNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting task: {ex.Message}");
        }
    }
}