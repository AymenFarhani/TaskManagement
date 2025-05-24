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
            Priority = viewModel.Priority
        };
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        
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
        {
            task.Title = viewModel.Title;
            task.Description = viewModel.Description;
            task.CreatedAt = viewModel.CreatedAt;
            task.Priority = viewModel.Priority;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("TaskList", "Task");
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