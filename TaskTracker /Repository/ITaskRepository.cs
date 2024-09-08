using TaskTracker.App;

namespace TaskTracker.Repository;

public interface ITaskRepository
{
    Task<List<TaskModel>> GetAllTasksAsync();
    Task<List<TaskModel>> GetByStatus(Task_Status status);
    void UpdateTaskStatus(int id, Task_Status status);
    void SaveAllTasks(List<TaskModel> tasks);
    void AddTask(string description);
    void UpdateTask(int id, string description);
    void DeleteTask(int id);
}