using TaskTracker.App;
using TaskTracker.Repository;

namespace TaskTracker.User_Interface;

public class CommandLineInterface
{
    private readonly JsonTaskRepository _repository;

    public CommandLineInterface(JsonTaskRepository repository)
    {
        _repository = repository;
    }

    public void ProcessCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Error: No command provided.");
            return;
        }
        
        string command = args[0].ToLower();
        switch (command)
        {
            case "list":
                HandleListCommand(args.Skip(1).ToArray());
                break;
            case "add":
                HandleAddCommand(args.Skip(1).ToArray());
                break;
            case "delete":
                HandleDeleteCommand(args.Skip(1).ToArray());
                break;
            case "update":
                HandleUpdateCommand(args.Skip(1).ToArray());
                break;
            case "mark-done":
                HandleUpdateStatusCommand(args.ToArray());
                break;
            case "mark-in-progress":
                HandleUpdateStatusCommand(args.ToArray());
                break;
            case "exit":
                break;
            default:
                Console.WriteLine($"Error: Unknown command '{command}'.");
                break;
        }
    }
    
    private async void HandleListCommand(string [] args)
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
                    var toDoTasks = await _repository.GetByStatus(Task_Status.Todo);
                    ShowTasks(toDoTasks);
                    break;
                case "done":
                    var doneTasks = await _repository.GetByStatus(Task_Status.Done);
                    ShowTasks(doneTasks);
                    break;
                case "in-progress":
                    var inProgressTasks = await _repository.GetByStatus(Task_Status.InProgress);
                    ShowTasks(inProgressTasks);
                    break;
                default:
                    Console.WriteLine($"Error: Unknown status '{args[0]}'.");
                    break;
            }
        }
    }

    private static void ShowTasks(List<TaskModel> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("There are no tasks, you can add new one :)");
        }
        var orderedTasks = tasks.OrderBy(t => t.Id);
        foreach (var task in orderedTasks)
        {
            Console.WriteLine(
                $"{task.Id} | {task.Description} | {task.Status} | {task.CreatedAt} | {task.UpdatedAt}");
        }
    }
    
    private void HandleAddCommand(string [] args)
    {
        if (args.Length== 0)
        {
            Console.WriteLine("Error: No Description were provided");
            return;
        }

        var description = string.Join(" ", args);
        _repository.AddTask(description);
    }
    
    private void HandleDeleteCommand(string [] args)
    {
        int taskId = int.Parse(args[0]);
        _repository.DeleteTask(taskId);
    }
    
    private void HandleUpdateCommand(string [] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Error: Both ID and description must be provided.");
            return;
        }
        if (!int.TryParse(args[0], out int taskId))
        {
            Console.WriteLine("Error: The first argument must be a valid numeric ID.");
            return;
        }
        if (string.IsNullOrWhiteSpace(args[1]))
        {
            Console.WriteLine("Error: The description must not be empty.");
            return;
        }
        
        string description = string.Join(" ", args);
        _repository.UpdateTask(taskId, description);
    }
    
    private void HandleUpdateStatusCommand(string [] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Error: Both ID and Status must be provided.");
            return;
        }
        if (!int.TryParse(args[1], out int taskId))
        {
            Console.WriteLine("Error: The first argument must be a valid numeric ID.");
            return;
        }
        if (string.IsNullOrWhiteSpace(args[0]))
        {
            Console.WriteLine("Error: The status must not be empty.");
            return;
        }

        switch (args[0].ToLower())
        {
            case "mark-done":
                _repository.UpdateTaskStatus(taskId, Task_Status.Done);
                break;
            case "mark-in-progress":
                _repository.UpdateTaskStatus(taskId,Task_Status.InProgress);
                break;
            default:
                Console.WriteLine($"Error: Unknown status '{args[0]}'.");
                break;
        }
    }
}