using VroomParts.Models;

namespace VroomParts.Areas.Admin.Application.CarParts
{
    public interface ICarPartService
    {
        List<CarPartDTO> GetAllCarParts();
        CarPartDTO GetById(Guid id);
        CarPartDTO CreateCarPart(CarPartDTO carPart);
        CarPartDTO EditCarPart(Guid id, CarPartDTO carPart);
        CarPartDTO DeleteCarPart(Guid id);
        bool CarPartExists(Guid id);
        List<CarPartDTO> GetList(GetPartsRequest request);
/*        ShoppingCart ToShoppingCart(Guid id);*/
        List<CarPartDTO> FilterCarPartsData(GetPartsRequest request);

    }
}
