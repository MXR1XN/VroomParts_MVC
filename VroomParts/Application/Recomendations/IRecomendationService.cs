namespace VroomParts.Application.Recomendations
{
    public interface IRecomendationService
    {
        void AddRecomendation(CreateRecomendationRequest create);
        void RemoveRecomendation(DeleteRecomendationRequest delete);
        void EditRecomendation(EditRecomendationRequest edit);
        RecomendationDto Find(GetRecomendationRequest get);
        List<RecomendationDto> GetRecomendations();
    }
}
