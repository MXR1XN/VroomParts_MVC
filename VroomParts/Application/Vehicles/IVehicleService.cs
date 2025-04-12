using VroomParts.Areas.Admin.ViewModels;

namespace VroomParts.Application.Vehicles
{
    public interface IVehicleService
    {
        void AddVehicle(VehicleViewModel model);
        void RemoveVehicle(Guid Id);
        void Edit(VehicleViewModel model, Guid Id);
        List<VehicleDto> GetVehicles();
        VehicleDto GetVehicle(Guid id);
    }
}
