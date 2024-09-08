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

    public async Task Execute(string[] args)
    {
        var description = string.Join(" ", args);
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Error: No Description were provided");
            return;
        }

        try
        {
            await _repository.AddTaskAsync(description);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding task: {ex.Message}");
        }

    }
}