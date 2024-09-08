namespace TaskTracker.Exceptions;

public class TaskNotFoundException: Exception
{
    public int TaskId { get; set; }
    public TaskNotFoundException(int taskId) 
        : base($"Task with id {taskId} was not found")
    {
        TaskId = taskId;
    }
    
    public TaskNotFoundException(int taskId, string message) 
        : base($"Task with id {taskId} was not found. more info: {message}")
    {
        TaskId = taskId;
    }
}