namespace TaskManagement.Models.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }


        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    }
}