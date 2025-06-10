using HabitTracker.Api.Infrastructure;
using HabitTracker.Api.Infrastructure.Entities;
using HabitTracker.Api.Models;
using HabitTracker.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly HabitTrackerDbContext _db;
    public TaskController(HabitTrackerDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _db.Tasks.Include(t => t.User).ToListAsync();
        var result = tasks.Select(t => new TaskDTO
        {
            TaskId = t.Id,
            Name = t.Name,
            Completed = t.Completed,
            CreatedByUserId = Guid.TryParse(t.CreatedByUserId, out var userId) ? userId : Guid.Empty,
            HasChildTasks = t.HasChildTasks,
            CreatedDate = t.CreatedDate,
            ParentListId = t.ParentListId,
            CompletedByUserId = t.CompletedByUserId,
            Notes = t.Notes,
            DueDate = t.DueDate
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        var t = await _db.Tasks.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        if (t == null) return NotFound();
        var dto = new TaskDTO
        {
            TaskId = t.Id,
            Name = t.Name,
            Completed = t.Completed,
            CreatedByUserId = Guid.TryParse(t.CreatedByUserId, out var userId) ? userId : Guid.Empty,
            HasChildTasks = t.HasChildTasks,
            CreatedDate = t.CreatedDate,
            ParentListId = t.ParentListId,
            CompletedByUserId = t.CompletedByUserId,
            Notes = t.Notes,
            DueDate = t.DueDate
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var task = new Infrastructure.Entities.Task
        {
            Id = dto.TaskId != Guid.Empty ? dto.TaskId : Guid.NewGuid(),
            Name = dto.Name,
            Completed = dto.Completed,
            CreatedByUserId = dto.CreatedByUserId.ToString(),
            CreatedDate = dto.CreatedDate == default ? DateTime.UtcNow : dto.CreatedDate,
            HasChildTasks = dto.HasChildTasks,
            ParentListId = dto.ParentListId,
            CompletedByUserId = dto.CompletedByUserId,
            Notes = dto.Notes,
            DueDate = dto.DueDate,
            User = _db.Users.FirstOrDefault(u => u.Id == dto.CreatedByUserId.ToString()) ?? new User { Id = dto.CreatedByUserId.ToString() }
        };
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskDTO dto)
    {
        var task = await _db.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return NotFound();
        task.Name = dto.Name;
        task.Completed = dto.Completed;
        task.HasChildTasks = dto.HasChildTasks;
        task.CreatedDate = dto.CreatedDate;
        task.ParentListId = dto.ParentListId;
        task.CompletedByUserId = dto.CompletedByUserId;
        task.Notes = dto.Notes;
        task.DueDate = dto.DueDate;
        // Optionally update User if needed
        await _db.SaveChangesAsync();
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return NotFound();
        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
