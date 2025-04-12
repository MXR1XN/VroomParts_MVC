using VroomParts.Domain.Products;

namespace VroomParts.Domain.Car
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public List<CarPart> Recommendations { get; set; } = new ();
    }
}
