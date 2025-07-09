using Microsoft.AspNetCore.Identity;

namespace RacingDigital.DAL.Models
{
    public class Horse
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Colour { get; set; }
        public int IdentityUserId { get; set; }
        public IdentityUser Owner { get; set; } = new IdentityUser();
    }
}