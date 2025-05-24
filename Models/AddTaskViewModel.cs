using TaskManagement.Models.Entities;

namespace TaskManagement.Models;
public class AddTaskViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
}