using Microsoft.EntityFrameworkCore;
using TaskManagement.Models.Entities;
namespace TaskManagement.Database
{
    public class TaskDBContext: DbContext
    {

        public TaskDBContext(DbContextOptions<TaskDBContext> options) : base(options)
        {
        }
        public DbSet<TaskItem> Tasks { get; set; } = null!;

}
      
}