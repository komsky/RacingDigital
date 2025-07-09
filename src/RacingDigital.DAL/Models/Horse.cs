using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RacingDigital.DAL.Models
{
    public class Horse
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Colour { get; set; }
        public string IdentityUserId { get; set; } = string.Empty; 
        public IdentityUser Owner { get; set; } = new IdentityUser();
    }
}