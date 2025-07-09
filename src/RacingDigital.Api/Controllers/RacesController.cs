using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingDigital.DAL;
using RacingDigital.DAL.Models;

namespace RacingDigital.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RacesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public RacesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RaceResult>>> Get()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var results = await _context.RaceResults
            .Include(r => r.Horse)
            .Include(r => r.Jockey)
            .Include(r => r.Racecourse)
            .Where(r => r.Horse.IdentityUserId == userId)
            .ToListAsync();

        return Ok(results);
    }
}
