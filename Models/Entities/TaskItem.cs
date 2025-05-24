using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }


        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateOnly DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public Status Status { get; set; } = Status.ToDo;
        
        [Display(Name = "Last Modified")]
    public DateTime LastModified { get; set; } = DateTime.UtcNow;


    }
}