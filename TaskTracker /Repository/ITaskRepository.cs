using TaskTracker.App;
using TaskTracker.Domain;

namespace TaskTracker.Repository;

public interface ITaskRepository
{
    Task<List<TaskModel>> GetAllTasksAsync();
    Task<List<TaskModel>> GetByStatusAsync(StatusTask status);
    Task UpdateTaskStatusAsync(int id, StatusTask status);
    Task SaveAllTasksAsync(List<TaskModel> tasks);
    Task AddTaskAsync(string description);
    Task UpdateTaskAsync(int id, string description);
    Task DeleteTaskAsync(int id);
}