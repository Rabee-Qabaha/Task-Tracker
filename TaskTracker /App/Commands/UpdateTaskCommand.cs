using TaskTracker.App.Interfaces;
using TaskTracker.Exceptions;
using TaskTracker.Repository;

namespace TaskTracker.App.Commands;

public class UpdateTaskCommand: ICommand
{
    private readonly JsonTaskRepository _repository;

    public UpdateTaskCommand(JsonTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(string[] args)
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

        try
        {
            await _repository.UpdateTaskAsync(taskId, description);
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