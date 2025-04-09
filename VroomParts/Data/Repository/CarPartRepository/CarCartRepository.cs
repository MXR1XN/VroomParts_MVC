using VroomParts.Domain;
using VroomParts.Domain.Products;

namespace VroomParts.Data.Repository.CarPartRepository
{
    public class CarCartRepository : Repository<CarPart>, ICarPartRepository
    {
        private readonly ApplicationDBContext _context;

        public CarCartRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public CarPart? Find(Guid id)
        {
            return _context.CarParts.FirstOrDefault(c => c.Id == id);
        }
    }
}
