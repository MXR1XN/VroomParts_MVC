namespace VroomParts.Domain.MissingRecommendations
{
    public interface IMissingRecommendationRepository : IRepository<MissingRecommendation>, IReadByIdRepository<Guid, MissingRecommendation>
    {

    }
}
