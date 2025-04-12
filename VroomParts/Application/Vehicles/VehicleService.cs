using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Domain.Car;

namespace VroomParts.Application.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository) 
        {
            _vehicleRepository = vehicleRepository;
        }

        public void AddVehicle(VehicleViewModel model)
        {
            var entity = new Vehicle() 
            { 
                Id = Guid.NewGuid(),
                Make = model.Make,
                Model = model.Model,
                Year = model.Year
            };

            _vehicleRepository.Create(entity);

        }

        public void Edit(VehicleViewModel model, Guid Id)
        {
            var entity = _vehicleRepository.Find(Id);

            if (entity == null) return;

            entity!.Model = model.Model;
            entity.Make = model.Make;
            entity.Year = model.Year;

            _vehicleRepository.Update(entity);
        }

        public VehicleDto GetVehicle(Guid Id)
        {
            var entity = _vehicleRepository.Find(Id);

            if (entity == null)
                throw new Exception("Vehicle not found");

            return entity!.ToDto();
        }

        public List<VehicleDto> GetVehicles()
        {
            return _vehicleRepository.Query().Select(v => v.ToDto()).ToList();
        }

        public void RemoveVehicle(Guid Id)
        {
            var entity = _vehicleRepository.Find(Id);
            if (entity != null) 
            {
                _vehicleRepository.Delete(entity);
            }
        }
    }
}
