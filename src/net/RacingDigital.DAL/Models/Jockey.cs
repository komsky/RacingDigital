using System.ComponentModel.DataAnnotations;

namespace RacingDigital.DAL.Models
{
    public class Jockey
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Country { get; set; } = null;

    }
}