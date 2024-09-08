using System.Text.Json;
using TaskTracker.Domain;
using TaskTracker.Exceptions;
using TaskTracker.Utils;

namespace TaskTracker.Repository;

public class JsonTaskRepository: ITaskRepository
{
    private readonly string _filePath;

    public JsonTaskRepository()
    {
        _filePath = FileHelper.GetFilePath("Tasks.json");
        FileHelper.EnsureFileExists(_filePath);
    }
    
    public async Task<List<TaskModel>> GetAllTasksAsync()
    {
        try
        {
            var json =await FileHelper.ReadFileAsync(_filePath);
            return JsonSerializer.Deserialize<List<TaskModel>>(json) ?? new List<TaskModel>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error deserializing tasks from JSON: {ex.Message}");
            return [];
        }
    }

    public async Task<List<TaskModel>> GetByStatusAsync(StatusTask status)
    {
        var tasks = await GetAllTasksAsync();
        return tasks.Where(t=> t.Status==status).ToList();
    }

    public async Task UpdateTaskStatusAsync(int id, StatusTask status)
    {
        var tasks = await GetAllTasksAsync();
        var taskToUpdate = tasks.Find(t => t.Id == id);
        
        if (taskToUpdate == null)
        {
            throw new TaskNotFoundException(id);
        }
        
        taskToUpdate.Status = status;
        taskToUpdate.UpdatedAt = DateTime.Now;
        
        await SaveAllTasksAsync(tasks);

        Console.WriteLine($"the task with id: {id} status Updated to: {status}");
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        var tasks = await GetAllTasksAsync();
        var task = tasks.Find(t => t.Id == id);

        if (task == null)
        {
            throw new TaskNotFoundException(id);
        }
        
        return task;
    }

    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    public async Task SaveAllTasksAsync(List<TaskModel> tasks)
    {
        await Semaphore.WaitAsync();
        try
        {
            var option = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(tasks, option);
            await FileHelper.WriteFileAsync(_filePath, json);
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task AddTaskAsync(string description)
    {
        var tasks = await GetAllTasksAsync();
        var taskId = tasks.Count != 0 ? tasks.Max(t=>t.Id) + 1 : 1;

        var task = new TaskModel()
        {
            Id = taskId,
            Description = description
        };
        
        tasks.Add(task);
        await SaveAllTasksAsync(tasks);
        
        Console.WriteLine($"Task added successfully with ID: {taskId}");
    }

    public async Task UpdateTaskAsync(int id, string description)
    {
        var tasks = await GetAllTasksAsync();
        var taskToUpdate = tasks.Find(t => t.Id == id);

        if (taskToUpdate == null)
        {
            throw new TaskNotFoundException(id);
        }
        
        taskToUpdate.Description = description;
        taskToUpdate.UpdatedAt= DateTime.Now;
        
        await SaveAllTasksAsync(tasks);
        
        Console.WriteLine($"Task with ID: {id} Updated successfully");
    }

    public async Task DeleteTaskAsync(int id)
    {
        var tasks = await GetAllTasksAsync();
        var taskToDelete = tasks.FirstOrDefault(t => t.Id == id);
    
        if (taskToDelete == null)
        {
            throw new TaskNotFoundException(id);
        }
        
        tasks.Remove(taskToDelete);
        await SaveAllTasksAsync(tasks);
        
        Console.WriteLine($"Task Task with ID: {id} successfully Deleted!");
    }
}