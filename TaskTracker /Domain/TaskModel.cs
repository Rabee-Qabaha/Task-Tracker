
namespace TaskTracker.Domain;

public class TaskModel
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public StatusTask Status { get; set; } = StatusTask.Todo;
    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public DateTime UpdatedAt { get; set; }= DateTime.Now;
    
}