namespace VroomParts.Application.Recomendations
{
    public interface IRecomendationService
    {
        void AddRecomendation(CreateRecomendationRequest create);
        void RemoveRecomendation(DeleteRecomendationRequest delete);
        void RemoveMissingRecomendation(Guid missingRecomendationId);
        List<RecomendationDto> GetPartsByVehicle(SearchRecomendationRequest model);
        List<RecomendationDto> GetRecomendations();
        List<MissingRecommendationDto> MissingRecommendations();
    }
}
