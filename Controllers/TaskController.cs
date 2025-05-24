using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Database;
using TaskManagement.Models;
using TaskManagement.Models.Entities;

public class TaskController : Controller
{

    private readonly TaskDBContext _context;

    public TaskController(TaskDBContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult AddTask()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddTaskAsync(AddTaskViewModel viewModel)
    {
        var task = new TaskItem
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            CreatedAt = viewModel.CreatedAt,
            Priority = viewModel.Priority,
            Status = viewModel.Status,
            DueDate = viewModel.DueDate,
            LastModified = DateTime.UtcNow
        };
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Task added successfully!";
        return RedirectToAction("TaskList", "Task");
    }

    [HttpGet]
    public async Task<IActionResult> TaskList()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return View(tasks);
    }

    [HttpGet]
    public async Task<IActionResult> EditTask(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> EditTask(TaskItem viewModel)
    {
        var task = await _context.Tasks.FindAsync(viewModel.Id);
        if (task == null)
        {
            return NotFound();
        }

        var logs = new List<TaskActivityLog>();


        void CheckChange(string fieldName, string? oldValue, string? newValue)
        {
            if (oldValue != newValue)
            {
                logs.Add(new TaskActivityLog
                {
                    TaskId = task.Id,
                    FieldChanged = fieldName,
                    OldValue = oldValue ?? string.Empty,
                    NewValue = newValue ?? string.Empty,
                    ModifiedAt = DateTime.UtcNow
                });
            }
        }


        CheckChange("Title", task.Title, viewModel.Title);
        CheckChange("Description", task.Description, viewModel.Description);
        CheckChange("Priority", task.Priority.ToString(), viewModel.Priority.ToString());
        CheckChange("Status", task.Status.ToString(), viewModel.Status.ToString());


        task.Title = viewModel.Title;
        task.Description = viewModel.Description;
        task.CreatedAt = viewModel.CreatedAt;
        task.Priority = viewModel.Priority;
        task.Status = viewModel.Status;
        task.DueDate = viewModel.DueDate;
        task.LastModified = DateTime.UtcNow;


        if (logs.Count > 0)
        {
            await _context.TaskActivityLogs.AddRangeAsync(logs);
        }

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Task updated successfully!";

        return RedirectToAction("TaskList", "Task");
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskHistory(Guid taskId)
    {
        var logs = await _context.TaskActivityLogs
            .Where(log => log.TaskId == taskId)
            .OrderByDescending(log => log.ModifiedAt)
            .Select(log => new
            {
                log.FieldChanged,
                log.OldValue,
                log.NewValue,
                ModifiedAt = log.ModifiedAt.ToString("yyyy-MM-dd HH:mm")
            })
            .ToListAsync();

        return Json(logs);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return RedirectToAction("TaskList", "Task");
    }
}