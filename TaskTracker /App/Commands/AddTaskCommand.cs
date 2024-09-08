using TaskTracker.App.Interfaces;
using TaskTracker.Repository;

namespace TaskTracker.App.Commands;

public class AddTaskCommand: ICommand
{
    private readonly JsonTaskRepository _repository;

    public AddTaskCommand(JsonTaskRepository repository)
    {
        _repository = repository;
    }

    public void Execute(string[] args)
    {
        var description = string.Join(" ", args);
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Error: No Description were provided");
            return;
        }
        
        _repository.AddTask(description);
    }
}