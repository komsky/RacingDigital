using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RacingDigital.DAL;
using RacingDigital.Seeding;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using WebApp_OpenIDConnect_DotNet.Models;

namespace WebApp_OpenIDConnect_DotNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }
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
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}