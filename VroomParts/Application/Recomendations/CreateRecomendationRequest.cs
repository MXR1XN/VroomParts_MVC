namespace VroomParts.Application.Recomendations
{
    public class CreateRecomendationRequest
    {
        public Guid CarId { get; set; }
        public Guid PartId { get; set; }
    }
}
