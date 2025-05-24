namespace TaskManagement.Models.Entities
{
    public class TaskActivityLog
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }

    public string FieldChanged { get; set; } = null!;
    public string OldValue { get; set; } = null!;
    public string NewValue { get; set; } = null!;

    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
}
}