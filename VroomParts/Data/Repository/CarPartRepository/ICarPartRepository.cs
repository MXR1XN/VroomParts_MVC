using VroomParts.Models.Product;

namespace VroomParts.Data.Repository.CarPartRepository
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
