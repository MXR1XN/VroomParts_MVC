namespace VroomParts.Application.Recomendations
{
    public class GetRecomendationRequest
    {
        public Guid CarId { get; set; }
        public Guid PartId { get; set; }
    }
}
