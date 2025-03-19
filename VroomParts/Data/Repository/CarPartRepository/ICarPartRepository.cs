using VroomParts.Models;

namespace VroomParts.Data.Repository.IRepository
{
    public interface ICarPartRepository
    {
        List<CarPart> GetAll();
        CarPart GetById(Guid id);
        CarPart CreateCarPart(CarPart carPart);
        CarPart UpdateCarPart(CarPart carPart);
        CarPart DeleteCarPart(CarPart carPart);
        IQueryable<CarPart> Query();
    }
}
