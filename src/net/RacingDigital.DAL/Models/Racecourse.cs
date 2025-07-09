using System.ComponentModel.DataAnnotations;

namespace RacingDigital.DAL.Models
{
    public class Racecourse
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}