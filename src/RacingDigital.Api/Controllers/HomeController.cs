using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingDigital.Api.Models;
using RacingDigital.DAL;
using RacingDigital.Seeding;

namespace RacingDigital.Api.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager )
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    //run migrations via action
    public IActionResult Migrate()
    {
        try
        {
            _context.Database.Migrate();
            return Content("Migrations applied successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while applying migrations.");
            return StatusCode(500, "An error occurred while applying migrations.");
        }
    }

    [Authorize]
    public IActionResult SeedData()
    {
        try
        {
            var userID = _userManager.GetUserId(User);
            var seeder = new DataSeeder(_context);
            var csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seed", "races.csv");
            seeder.SeedFromCsv(csvPath, userID);
            return Content("Data seeded successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding data.");
            return StatusCode(500, "An error occurred while seeding data.");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
