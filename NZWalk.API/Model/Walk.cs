namespace NZWalk.API.Model
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Double LengthInKm { get; set; }
        public string? ImageURL { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        public Region region { get; set; }
        public Difficulty difficulty { get; set; }

        public Walk() { }

    }
}
