using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingDigital.DAL;
using RacingDigital.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RacingDigital.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RacesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RacesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RaceResult>>> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var results = await _context.RaceResults
            .Include(r => r.Horse)
            .Include(r => r.Jockey)
            .Include(r => r.Racecourse)
            .Include(r => r.Trainer)
            .Include(r => r.Notes)
            .ToListAsync();

        return Ok(results);
    }

    [HttpPost("{id}/notes")]
    public async Task<ActionResult<Note>> AddNote(int id, [FromBody] Note note)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var raceResult = await _context.RaceResults
            .Include(r => r.Notes)
            .FirstOrDefaultAsync(r => r.ID == id);

        if (raceResult == null)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(userId);

        var newNote = new Note
        {
            Content = note.Content,
            RaceResultId = raceResult.ID
        };

        _context.Notes.Add(newNote);
        await _context.SaveChangesAsync();

        return Ok(newNote);
    }
}
