using CsvHelper;
using CsvHelper.Configuration;
using RacingDigital.DAL;
using RacingDigital.DAL.Models;
using System.Globalization;

namespace RacingDigital.Seeding
{
    public class RaceCsvRecord
    {
        public string Race { get; set; } = string.Empty;
        public string RaceDate { get; set; } = string.Empty;      // dd/MM/yyyy
        public string RaceTime { get; set; } = string.Empty;      // HHmm
        public string Racecourse { get; set; } = string.Empty;
        public string RaceDistance { get; set; } = string.Empty;  // not used
        public string Jockey { get; set; } = string.Empty;
        public string Trainer { get; set; } = string.Empty;       // not used
        public string Horse { get; set; } = string.Empty;
        public int FinishingPosition { get; set; }
        public string DistanceBeaten { get; set; } = string.Empty; // not used
        public string TimeBeaten { get; set; } = string.Empty;     // not used
    }

    // 2. (Optional) explicit mapping if your CSV headers differ
    public sealed class RaceCsvMap : ClassMap<RaceCsvRecord>
    {
        public RaceCsvMap()
        {
            Map(m => m.Race).Name("Race");
            Map(m => m.RaceDate).Name("RaceDate");
            Map(m => m.RaceTime).Name("RaceTime");
            Map(m => m.Racecourse).Name("Racecourse");
            Map(m => m.RaceDistance).Name("RaceDistance");
            Map(m => m.Jockey).Name("Jockey");
            Map(m => m.Trainer).Name("Trainer");
            Map(m => m.Horse).Name("Horse");
            Map(m => m.FinishingPosition).Name("FinishingPosition");
            Map(m => m.DistanceBeaten).Name("DistanceBeaten");
            Map(m => m.TimeBeaten).Name("TimeBeaten");
        }
    }

    // 3. Seeder class
    public class DataSeeder
    {
        private readonly ApplicationDbContext _db;

        public DataSeeder(ApplicationDbContext context)
        {
            _db = context;
        }

        public void SeedFromCsv(string csvFilePath, string userId)
        {
            // Avoid re-seeding
            if (_db.RaceResults.Any()) return;

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<RaceCsvMap>();
            var records = csv.GetRecords<RaceCsvRecord>().ToList();

            foreach (var r in records)
            {
                if (!DateTime.TryParseExact(r.RaceDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                             DateTimeStyles.None, out var raceDate))
                {
                    continue;
                }

                // 1) Racecourse
                var course = _db.Racecourses.FirstOrDefault(x => x.Name == r.Racecourse);
                if (course == null)
                {
                    course = new Racecourse { Name = r.Racecourse };
                    _db.Racecourses.Add(course);
                }

                // 2) Jockey
                var jockey = _db.Jockeys.FirstOrDefault(x => x.Name == r.Jockey);
                if (jockey == null)
                {
                    jockey = new Jockey
                    {
                        Name = r.Jockey,
                        Country = null // assuming no country data is provided
                    };
                    _db.Jockeys.Add(jockey);
                }

                // 3) Horse
                var horse = _db.Horses.FirstOrDefault(x => x.Name == r.Horse);
                if (horse == null)
                {
                    horse = new Horse
                    {
                        Name = r.Horse,
                        IdentityUserId = userId, // assuming you have a user ID to assign 
                    };
                    _db.Horses.Add(horse);
                }

                // 4) RaceResult
                var result = new RaceResult
                {
                    RaceName = r.Race,
                    RaceDate = raceDate,
                    Racecourse = course,
                    Jockey = jockey,
                    Horse = horse,
                    FinishingPosition = r.FinishingPosition,
                    DistanceBeaten = r.DistanceBeaten,
                    TimeBeaten = r.TimeBeaten,
                };

                _db.RaceResults.Add(result);
            }

            _db.SaveChanges();
        }
    }
}
