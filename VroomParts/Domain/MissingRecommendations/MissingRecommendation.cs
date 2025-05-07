namespace VroomParts.Domain.MissingRecommendations
{
    public class MissingRecommendation
    {
        public Guid Id { get; set; }

        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsResolved { get; set; } = false;
    }
}
