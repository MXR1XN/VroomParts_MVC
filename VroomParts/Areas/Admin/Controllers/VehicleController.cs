using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VroomParts.Application.Vehicles;
using VroomParts.Areas.Admin.ViewModels;
using VroomParts.Utility;

namespace VroomParts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService) 
        {
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            var vehicles = _vehicleService.GetVehicles();

            var model = vehicles.Select(c => new VehicleViewModel() { Id = c.Id, Model = c.Model, Make = c.Make, Year = c.Year }).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VehicleViewModel vehicle)
        {
            if (!ModelState.IsValid)
            {
                return View(vehicle);
            }

            _vehicleService.AddVehicle(vehicle);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var vehicle = _vehicleService.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var model = new VehicleViewModel()
            {
                Model = vehicle.Model,
                Make = vehicle.Make,
                Year = vehicle.Year
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, VehicleViewModel vehicle)
        {
            if (!ModelState.IsValid)
            {
                return View(vehicle);
            }

            try
            {
                _vehicleService.Edit(vehicle, id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var vehicle = _vehicleService.GetVehicle(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var model = new VehicleViewModel()
            {
                Model = vehicle.Model,
                Make = vehicle.Make,
                Year = vehicle.Year
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _vehicleService.RemoveVehicle(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
