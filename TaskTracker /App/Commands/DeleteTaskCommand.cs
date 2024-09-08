using TaskTracker.App.Interfaces;
using TaskTracker.Exceptions;
using TaskTracker.Repository;

namespace TaskTracker.App.Commands;

public class DeleteTaskCommand: ICommand
{
    private readonly JsonTaskRepository _repository;

    public DeleteTaskCommand(JsonTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Error: Please provide a task ID.");
            return;
        }
        
        if (!int.TryParse(args[0], out int taskId))
        {
            Console.WriteLine("Error: Please provide a valid task ID.");
            return;
        }

        try
        {
            await _repository.DeleteTaskAsync(taskId);
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