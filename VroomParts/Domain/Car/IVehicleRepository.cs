namespace VroomParts.Domain.Car
{
    public interface IVehicleRepository :IRepository<Vehicle>, IReadByIdRepository<Guid, Vehicle> 
    {
    }
}
