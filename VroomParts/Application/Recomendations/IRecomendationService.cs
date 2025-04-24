namespace VroomParts.Application.Recomendations
{
    public interface IRecomendationService
    {
        void AddRecomendation(CreateRecomendationRequest create);
        void RemoveRecomendation(DeleteRecomendationRequest delete);
        List<RecomendationDto> GetRecomendationsByModel(SearchRecomendationRequest model);
        List<RecomendationDto> GetRecomendations();
    }
}
