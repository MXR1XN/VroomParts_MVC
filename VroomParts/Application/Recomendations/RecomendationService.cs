
using VroomParts.Domain.Car;
using VroomParts.Domain.Products;
using VroomParts.Domain.VehicleRecommendations;

namespace VroomParts.Application.Recomendations
{
    public class RecomendationService : IRecomendationService
    {
        private readonly IRecomendationRepository _recomendationRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICarPartRepository _carPartRepository;
        public RecomendationService(
            IRecomendationRepository recomendationRepository,
            IVehicleRepository vehicleRepository,
            ICarPartRepository carPartRepository
            ) 
        {
            _recomendationRepository = recomendationRepository;
            _vehicleRepository = vehicleRepository;
            _carPartRepository = carPartRepository;
        }

        public void AddRecomendation(CreateRecomendationRequest create)
        {
            var car = _vehicleRepository.Find(create.CarId);

            var part = _carPartRepository.Find(create.CarId);

            var recomendation = _recomendationRepository.Create(new VehicleRecommendation()
            {
                VehicleId = create.CarId,
                CarPartId = create.PartId
            });
        }

        public void EditRecomendation(EditRecomendationRequest edit)
        {
            var recomentaion = _recomendationRepository.Query().FirstOrDefault(r => r.VehicleId == edit.CarId && r.CarPartId == edit.PartId);

            if (recomentaion == null) throw new ArgumentException("Recommendation not found");

            var newPart = _carPartRepository.Find(edit.NewPartId);

            recomentaion.CarPartId = edit.NewPartId;

            _recomendationRepository.Update(recomentaion);
        }
        public void RemoveRecomendation(DeleteRecomendationRequest delete)
        {
            var recomentaion = _recomendationRepository.Query().FirstOrDefault(r => r.VehicleId == delete.CarId && r.CarPartId == delete.PartId);

            if (recomentaion == null) throw new ArgumentException("Recommendation not found");

            _recomendationRepository.Delete(recomentaion);
        }

        public RecomendationDto Find(GetRecomendationRequest get)
        {
            var recomentaion = _recomendationRepository.Query().FirstOrDefault(r => r.VehicleId == get.CarId && r.CarPartId == get.PartId);

            if (recomentaion == null) throw new ArgumentException("Recommendation not found");

            return recomentaion.ToDto();
        }

        public List<RecomendationDto> GetRecomendations()
        {
            return _recomendationRepository.Query().Select( r => r.ToDto()).ToList();
        }
    }
}
