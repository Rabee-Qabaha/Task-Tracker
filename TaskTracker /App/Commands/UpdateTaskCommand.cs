using TaskTracker.App.Interfaces;
using TaskTracker.Repository;

namespace TaskTracker.App.Commands;

public class UpdateTaskCommand: ICommand
{
    private readonly JsonTaskRepository _repository;

    public UpdateTaskCommand(JsonTaskRepository repository)
    {
        _repository = repository;
    }

    public void Execute(string[] args)
    {
        var description = args[1];

        if (!int.TryParse(args[0], out int taskId))
        {
            Console.WriteLine("Error: Please provide a valid task ID.");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Error: The description must not be empty.");
            return;
        }
        
        _repository.UpdateTask(taskId,description);
    }
}