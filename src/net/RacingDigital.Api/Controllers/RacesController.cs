using Microsoft.AspNetCore.Authorization;
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
            .ToListAsync();

        return Ok(results);
    }
}
