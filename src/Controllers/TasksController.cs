using HabitTracker.Api.Infrastructure;
using HabitTracker.Api.Models.DTOs;
using HabitTracker.Api.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly HabitTrackerDbContext _db;
    public TasksController(HabitTrackerDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _db.Tasks.Include(t => t.CreatedByUser).Include(t => t.CompletedByUser).ToListAsync();
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized("User not found");

        var result = tasks.Where(t => t.CreatedByUser.Id == userId).Select(TaskDTO.ToTaskDTO).ToList();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        var t = await _db.Tasks.Include(t => t.CreatedByUser).Include(t => t.CompletedByUser).FirstOrDefaultAsync(x => x.Id == id);
        if (t == null) return NotFound();
        return Ok(TaskDTO.ToTaskDTO(t));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized("User not found");
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return Unauthorized("User not found");

        var task = new Infrastructure.Entities.Task
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Completed = false,
            CreatedByUser = user,
            CreatedDate = DateTime.UtcNow,
            HasChildTasks = false,
            ParentListId = request.ParentListId,
            CompletedByUser = null,
            Notes = request.Notes,
            DueDate = request.DueDate,
            Priority = request.Priority
        };
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, TaskDTO.ToTaskDTO(task));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskDTO dto)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return NotFound();

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == dto.CreatedByUserId.ToString());
        if (user == null) return BadRequest("User not found");

        task.Name = dto.Name;
        task.Completed = dto.Completed;
        task.HasChildTasks = dto.HasChildTasks;
        task.CreatedDate = dto.CreatedDate;
        task.ParentListId = dto.ParentListId;
        task.CompletedByUser = user;
        task.Notes = dto.Notes;
        task.DueDate = dto.DueDate;
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
