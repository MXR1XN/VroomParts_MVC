namespace VroomParts.Application.Recomendations
{
    public class DeleteRecomendationRequest
    {
        public Guid CarId { get; set; }
        public Guid PartId { get; set; }
    }
}
