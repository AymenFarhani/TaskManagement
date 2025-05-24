using TaskManagement.Models.Entities;

namespace TaskManagement.Models;
public class AddTaskViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public DateTime LastModified  { get; set; } = DateTime.UtcNow;

    public DateOnly DueDate { get; set; }  = DateOnly.FromDateTime(DateTime.UtcNow);

    public Status Status { get; set; } = Status.ToDo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
}