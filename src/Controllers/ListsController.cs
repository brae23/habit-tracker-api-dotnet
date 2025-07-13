using HabitTracker.Api.Infrastructure;
using HabitTracker.Api.Infrastructure.Entities;
using HabitTracker.Api.Models.DTOs;
using HabitTracker.Api.Models.Enums;
using HabitTracker.Api.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Api.Controllers;

[ApiController]
[Route("api/lists")]
[Authorize]
public class ListsController : ControllerBase
{
    private readonly HabitTrackerDbContext _db;
    public ListsController(HabitTrackerDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetLists()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized("User not found");

        var lists = await _db.Lists.Include(l => l.CreatedByUser).Include(t => t.Tasks).Include(l => l.Sublists).ThenInclude(sl => sl.Tasks).ToListAsync();
        var result = lists.Where(x => x.CreatedByUser.Id == userId && x.ParentListId == null).Select(ListDTO.ToListDTO);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetList(Guid id)
    {
        var list = await _db.Lists.Include(l => l.CreatedByUser).Include(t => t.Tasks).Include(l => l.Sublists).ThenInclude(sl => sl.Tasks).FirstOrDefaultAsync(l => l.Id == id);
        if (list == null) return NotFound();
        return Ok(ListDTO.ToListDTO(list));
    }

    [HttpGet("daily")]
    public async Task<IActionResult> GetDailyTaskList()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (userId == null || user == null) return Unauthorized("User not found");

        var list = await _db.Lists
            .Include(l => l.CreatedByUser)
            .Include(t => t.Tasks)
            .Include(l => l.Sublists)
            .ThenInclude(sl => sl.Tasks)
            .Where(l => l.Type == ListType.Daily && l.CreatedByUser.Id == userId)
            .OrderByDescending(l => l.CreatedDate)
            .FirstOrDefaultAsync();

        if (list == null)
        {
            var dateString = $"{DateTime.UtcNow:yyyy-MM-dd}";
            var newDtl = new List
            {
                Id = Guid.NewGuid(),
                Name = $"{dateString} Daily Tasks",
                CreatedDate = DateTime.UtcNow,
                Description = $"{dateString} Daily Tasks",
                CreatedByUser = user,
                ParentListId = null,
                Type = ListType.Daily
            };

            _db.Lists.Add(newDtl);
            await _db.SaveChangesAsync();
            return Ok(ListDTO.ToListDTO(newDtl));
        }

        return Ok(ListDTO.ToListDTO(list));
    }

    [HttpPost]
    public async Task<IActionResult> CreateList([FromBody] CreateListRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return Unauthorized("User not found");

        var list = new List
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedDate = DateTime.UtcNow,
            Description = request.Description,
            CreatedByUser = user,
            ParentListId = request.ParentListId,
            Type = request.Type
        };

        _db.Lists.Add(list);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetList), new { id = list.Id }, ListDTO.ToListDTO(list));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateList(Guid id, [FromBody] ListDTO dto)
    {
        var list = await _db.Lists.Include(t => t.Tasks).Include(l => l.Sublists).ThenInclude(sl => sl.Tasks).FirstOrDefaultAsync(l => l.Id == id);
        if (list == null) return NotFound();
        list.Name = dto.Name;
        list.Description = dto.Description;
        await _db.SaveChangesAsync();
        return Ok(ListDTO.ToListDTO(list));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteList(Guid id)
    {
        var list = await _db.Lists.Include(t => t.Tasks).Include(l => l.Sublists).ThenInclude(sl => sl.Tasks).FirstOrDefaultAsync(l => l.Id == id);
        if (list == null) return NotFound();
        _db.Lists.Remove(list);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}