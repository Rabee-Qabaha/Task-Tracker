using TaskTracker.App.Interfaces;
using TaskTracker.Repository;

namespace TaskTracker.App.Commands;

public class DeleteTaskCommand: ICommand
{
    private readonly JsonTaskRepository _repository;

    public DeleteTaskCommand(JsonTaskRepository repository)
    {
        _repository = repository;
    }

    public void Execute(string[] args)
    {
        if (!int.TryParse(args[0], out int taskId))
        {
            Console.WriteLine("Error: Please provide a valid task ID.");
            return;
        }
        
        _repository.DeleteTask(taskId);
    }
}