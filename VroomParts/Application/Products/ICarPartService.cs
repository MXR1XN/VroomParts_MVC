using VroomParts.Areas.Admin.ViewModels;

namespace VroomParts.Application.Products
{
    public interface ICarPartService
    {
        CarPartDTO GetById(Guid id);

        CarPartDTO Create(CreateCarPartModel model);

        CarPartDTO Edit(Guid id, CreateCarPartModel model);

        CarPartDTO Delete(Guid id);

        List<CarPartDTO> Search(GetPartsRequest request);

    }
}
