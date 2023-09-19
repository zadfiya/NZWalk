namespace NZWalk.API.Model.DTO.Walk
{
    public class AddWalkDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Double LengthInKm { get; set; }
        public string? ImageURL { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
