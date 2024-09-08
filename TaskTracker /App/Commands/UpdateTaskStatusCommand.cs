using TaskTracker.App.Interfaces;
using TaskTracker.Domain;
using TaskTracker.Repository;

namespace TaskTracker.App.Commands;

public class UpdateTaskStatusCommand: ICommand
{
    private readonly JsonTaskRepository _repository;

    public UpdateTaskStatusCommand(JsonTaskRepository repository)
    {
        _repository = repository;
    }
    
    public void Execute(string[] args)
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
        
        if (!int.TryParse(args[1], out int taskId))
        {
            Console.WriteLine("Error: The first argument must be a valid numeric ID.");
            return;
        }

        switch (args[0].ToLower())
        {
            case "done":
                _repository.UpdateTaskStatus(taskId, StatusTask.Done);
                break;
            case "in-progress":
                _repository.UpdateTaskStatus(taskId,StatusTask.InProgress);
                break;
            case "todo":
                _repository.UpdateTaskStatus(taskId,StatusTask.Todo);
                break;
            default:
                Console.WriteLine($"Error: Unknown status '{args[0]}'.");
                break;
        }
    }
}