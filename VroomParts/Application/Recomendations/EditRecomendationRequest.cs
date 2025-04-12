namespace VroomParts.Application.Recomendations
{
    public class EditRecomendationRequest
    {
        public Guid CarId { get; set; }
        public Guid PartId { get; set; }
        public Guid NewPartId { get; set; }
    }
}
