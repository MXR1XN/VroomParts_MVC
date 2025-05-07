using Microsoft.EntityFrameworkCore;
using VroomParts.Domain.MissingRecommendations;

namespace VroomParts.Data.Repository.MissingRecommendations
{
    public class MissingRecomendationRepository : Repository<MissingRecommendation>, IMissingRecommendationRepository
    {
        private readonly ApplicationDBContext _context;
        public MissingRecomendationRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public MissingRecommendation? Find(Guid id)
        {
            return _context.MissingRecommendations.FirstOrDefault(x => x.Id == id);
        }
    }
}
