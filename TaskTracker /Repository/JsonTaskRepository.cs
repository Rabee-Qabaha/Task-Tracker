using System.Text.Json;
using TaskTracker.App;
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
        var json =await FileHelper.ReadFileAsync(_filePath);
        return JsonSerializer.Deserialize<List<TaskModel>>(json) ?? new List<TaskModel>();
    }

    public async Task<List<TaskModel>> GetByStatus(Task_Status status)
    {
        var tasks = await GetAllTasksAsync();
        return tasks.Where(t=> t.Status==status).ToList();
    }

    public async void UpdateTaskStatus(int id, Task_Status status)
    {
        var tasks = await GetAllTasksAsync();
        var taskToUpdate = tasks.FirstOrDefault(t => t.Id == id);
        
        if (taskToUpdate == null)
        {
            throw new NullReferenceException($"Task with id: {id} not found");
        }
        
        taskToUpdate.Status = status;
        taskToUpdate.UpdatedAt = DateTime.Now;
        
        SaveAllTasks(tasks);

        Console.WriteLine($"the task with id: {id} status Updated to: {status}");
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        var tasks = await GetAllTasksAsync();
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            throw new NullReferenceException($"Task with id: {id} not found");
        }
        
        return task;
    }

    public void SaveAllTasks(List<TaskModel> tasks)
    {
        var option = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(tasks, option);
        FileHelper.WriteFile(_filePath, json);
    }

    public async void AddTask(string description)
    {
        var tasks = await GetAllTasksAsync();
        var taskId = tasks.Count != 0 ? tasks.Max(t=>t.Id) + 1 : 1;

        var task = new TaskModel()
        {
            Id = taskId,
            Description = description,
        };
        
        tasks.Add(task);
        SaveAllTasks(tasks);
        
        Console.WriteLine($"Task added successfully with ID: {taskId}");
    }

    public async void UpdateTask(int id, string description)
    {
        var tasks = await GetAllTasksAsync();
        var taskToUpdate = tasks.Find(t => t.Id == id);

        if (taskToUpdate == null)
        {
            throw new NullReferenceException($"Task with id: {id} not found");
        }
        
        taskToUpdate.Description = description;
        taskToUpdate.UpdatedAt= DateTime.Now;
        
        SaveAllTasks(tasks);
        
        Console.WriteLine($"Task with ID: {id} Updated successfully");
    }

    public async void DeleteTask(int id)
    {
        var tasks = await GetAllTasksAsync();
        await GetTaskByIdAsync(id);// to handel null ID, is can be improved
        
        tasks.RemoveAll(t=> t.Id == id);
        
        SaveAllTasks(tasks);
        
        Console.WriteLine($"Task Task with ID: {id} successfully Deleted!");
    }
}