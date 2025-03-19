using VroomParts.Data.Repository.IRepository;
using VroomParts.Models;

namespace VroomParts.Data.Repository.CarPartRepository
{
    public class CarCartRepository : ICarPartRepository
    {
        private readonly ApplicationDBContext _context;

        public CarCartRepository(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public CarPart CreateCarPart(CarPart carPart)
        {
            _context.CarParts.Add(carPart);
            _context.SaveChanges();
            return carPart;
        }

        public CarPart DeleteCarPart(CarPart carPart)
        {
            _context.Remove(carPart);
            _context.SaveChanges();
            return carPart;
        }

        public List<CarPart> GetAll()
        {
            return _context.CarParts.ToList();
        }

        public CarPart? GetById(Guid id)
        {
            var carPart = _context.CarParts.FirstOrDefault(j => j.Id == id);
            return carPart;
        }

        public IQueryable<CarPart> Query()
        {
            return _context.CarParts.AsQueryable();
        }

        public CarPart UpdateCarPart(CarPart carPart)
        {
            _context.Update(carPart);
            _context.SaveChanges();
            return carPart;
        }
    }
}
