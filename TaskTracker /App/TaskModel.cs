
namespace TaskTracker.App;

public class TaskModel
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public Task_Status Status { get; set; } = Task_Status.Todo;
    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public DateTime UpdatedAt { get; set; }= DateTime.Now;
    
}