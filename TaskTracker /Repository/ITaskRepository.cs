using TaskTracker.App;
using TaskTracker.Domain;

namespace TaskTracker.Repository;

public interface ITaskRepository
{
    Task<List<TaskModel>> GetAllTasksAsync();
    Task<List<TaskModel>> GetByStatus(StatusTask status);
    void UpdateTaskStatus(int id, StatusTask status);
    void SaveAllTasks(List<TaskModel> tasks);
    void AddTask(string description);
    void UpdateTask(int id, string description);
    void DeleteTask(int id);
}