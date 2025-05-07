using VroomParts.Domain.VehicleRecommendations;

namespace VroomParts.Data.Repository.RecomendationRepository
{
    public class RecomendationRepository : Repository<VehicleRecommendation>, IRecomendationRepository
    {
        public RecomendationRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext) 
        {
        }
    }
}
