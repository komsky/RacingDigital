using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RacingDigital.DAL.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int RaceResultId { get; set; }
    }
}