using Microsoft.AspNetCore.Identity;

namespace RacingDigital.DAL.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int RaceResultId { get; set; }
        public RaceResult RaceResult { get; set; } = new RaceResult();

        public int IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; } = new IdentityUser();
    }
}