using VroomParts.Domain.Car;

namespace VroomParts.Data.Repository.VehicleRepository
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public VehicleRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }

        public Vehicle? Find(Guid id)
        {
            return _dbContext.Vehicles.FirstOrDefault(x => x.Id == id);
        }
    }
}
