namespace RacingDigital.DAL.Models
{
    public class RaceResult
    {
        public int Id;
        public DateTime RaceDate;
        public string RaceName { get; set; } = string.Empty;
        public Racecourse Racecourse { get; set; } = new Racecourse();
        public Horse Horse { get; set; } = new Horse();
        public Jockey Jockey { get; set; } = new Jockey();
        public int FinishingPosition;
        // nav prop
        public ICollection<Note> Notes { get; set; } = [];
    }
}
