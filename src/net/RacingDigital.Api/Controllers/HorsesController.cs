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
public class HorsesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HorsesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Horse>>> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var results = await _context.Horses.Where(h => h.IdentityUserId == userId)
            .ToListAsync();

        return Ok(results);
    }

    [HttpGet("{horseId:int}/best-jockey")]
    public async Task<IActionResult> GetBestJockeyForHorse(int horseId)
    {
        // 1️  Pull every race this horse has run.
        // 2️  Group those races by jockey.
        // 3️  Compute simple performance metrics per jockey.
        // 4️  Pick the jockey with -> highest win-rate, then -> lowest average finishing position.
        var best = await _context.RaceResults
            .Where(r => r.Horse.ID == horseId)                   // Horse filter  :contentReference[oaicite:0]{index=0}
            .GroupBy(r => new { r.Jockey.ID, r.Jockey.Name })    // One “bucket” per jockey  :contentReference[oaicite:1]{index=1}
            .Select(g => new
            {
                JockeyId = g.Key.ID,
                JockeyName = g.Key.Name,
                Starts = g.Count(),
                Wins = g.Count(r => r.FinishingPosition == 1),
                WinRate = (double)g.Count(r => r.FinishingPosition == 1) / g.Count(),
                AvgFinish = g.Average(r => r.FinishingPosition)
            })
            .OrderByDescending(x => x.WinRate)   // best win-rate first
            .ThenBy(x => x.AvgFinish)            // tie-break: lower avg. placing
            .FirstOrDefaultAsync();              // null ⇒ no history for this horse

        return best is null ? NotFound("No race history for that horse.") : Ok(best);
    }

}
