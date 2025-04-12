using VroomParts.Domain.VehicleRecommendations;

namespace VroomParts.Data.Repository.RecomendationRepository
{
    public class RecomendationRepository : Repository<VehicleRecommendation>, IRecomendationRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public RecomendationRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext) 
        {
            _dbContext = applicationDBContext;
        }
    }
}
