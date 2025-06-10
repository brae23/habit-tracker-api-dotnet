using HabitTracker.Api.Infrastructure;
using HabitTracker.Api.Infrastructure.Entities;
using HabitTracker.Api.Models;
using HabitTracker.Api.Processors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ListController : ControllerBase
{
    private readonly HabitTrackerDbContext _db;
    public ListController(HabitTrackerDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetLists()
    {
        var lists = await _db.Lists.Include(l => l.ListItems).ToListAsync();
        var result = lists.Select(ListProcessor.ToListDTO);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetList(Guid id)
    {
        var list = await _db.Lists.Include(l => l.ListItems).FirstOrDefaultAsync(l => l.Id == id);
        if (list == null) return NotFound();
        return Ok(ListProcessor.ToListDTO(list));
    }

    [HttpPost]
    public async Task<IActionResult> CreateList([FromBody] ListDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var list = new List
        {
            Id = dto.ListId != Guid.Empty ? dto.ListId : Guid.NewGuid(),
            Name = dto.Name,
            CreatedDate = dto.CreatedDate == default ? DateTime.UtcNow : dto.CreatedDate,
            Description = dto.Description,
            ListItems = dto.ListItems?.Select(t => new Infrastructure.Entities.Task
            {
                Id = t.TaskId != Guid.Empty ? t.TaskId : Guid.NewGuid(),
                Name = t.Name,
                Completed = t.Completed,
                CreatedByUserId = t.CreatedByUserId.ToString(),
                CreatedDate = t.CreatedDate == default ? DateTime.UtcNow : t.CreatedDate,
                HasChildTasks = t.HasChildTasks,
                ParentListId = t.ParentListId,
                CompletedByUserId = t.CompletedByUserId,
                Notes = t.Notes,
                DueDate = t.DueDate,
                User = _db.Users.FirstOrDefault(u => u.Id == t.CreatedByUserId.ToString()) ?? new User { Id = t.CreatedByUserId.ToString() }
            }).ToList() ?? new List<Infrastructure.Entities.Task>()
        };
        _db.Lists.Add(list);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetList), new { id = list.Id }, ListProcessor.ToListDTO(list));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateList(Guid id, [FromBody] ListDTO dto)
    {
        var list = await _db.Lists.Include(l => l.ListItems).FirstOrDefaultAsync(l => l.Id == id);
        if (list == null) return NotFound();
        list.Name = dto.Name;
        list.Description = dto.Description;
        // Optionally update ListItems here
        await _db.SaveChangesAsync();
        return Ok(ListProcessor.ToListDTO(list));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteList(Guid id)
    {
        var list = await _db.Lists.Include(l => l.ListItems).FirstOrDefaultAsync(l => l.Id == id);
        if (list == null) return NotFound();
        _db.Lists.Remove(list);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}