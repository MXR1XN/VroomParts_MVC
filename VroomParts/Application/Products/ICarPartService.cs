using System.Net;
using VroomParts.Application.Recomendations;
using VroomParts.Areas.Admin.ViewModels;
namespace VroomParts.Application.Products
{
    public interface ICarPartService
    {
        CarPartDto GetById(Guid id);

        CarPartDto Create(CreateCarPartModel model);

        CarPartDto Edit(Guid id, CarPartViewModel model);

        CarPartDto Delete(Guid id);

        List<CarPartDto> Search(GetPartsRequest request);
        List<CarPartDto> GetByCompatibility(SearchRecomendationRequest compatibilityKey);
        List<CarPartDto> GetParts();

        List<CarPartDto> GetByViewCount();

    }
}
