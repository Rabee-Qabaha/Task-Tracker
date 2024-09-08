using TaskTracker.App.Interfaces;
using TaskTracker.Domain;
using TaskTracker.Exceptions;
using TaskTracker.Repository;

namespace TaskTracker.App.Commands;

public class GetListsCommand : ICommand
{
    private readonly JsonTaskRepository _repository;

    public GetListsCommand(JsonTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                var tasks = await _repository.GetAllTasksAsync();
                ShowTasks(tasks);
            }
            else if (args.Length == 1)
            {
                switch (args[0].ToLower())
                {
                    case "todo":
                        var toDoTasks = await _repository.GetByStatusAsync(StatusTask.Todo);
                        ShowTasks(toDoTasks);
                        break;
                    case "done":
                        var doneTasks = await _repository.GetByStatusAsync(StatusTask.Done);
                        ShowTasks(doneTasks);
                        break;
                    case "in-progress":
                        var inProgressTasks = await _repository.GetByStatusAsync(StatusTask.InProgress);
                        ShowTasks(inProgressTasks);
                        break;
                    default:
                        Console.WriteLine($"Error: Unknown status '{args[0]}'.");
                        break;
                }
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

    private static void ShowTasks(List<TaskModel> tasks)
    {
        if (tasks.Count == 0) Console.WriteLine("There are no tasks, you can add new one :)");
        var orderedTasks = tasks.OrderBy(t => t.Id);
        foreach (var task in orderedTasks)
            Console.WriteLine(
                $"{task.Id} | {task.Description} | {task.Status} | {task.CreatedAt} | {task.UpdatedAt}");
    }
}