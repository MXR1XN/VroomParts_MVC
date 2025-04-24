using VroomParts.Domain.Car;
using VroomParts.Domain.Categories;
using VroomParts.Domain.TrackViews;

namespace VroomParts.Domain.Products;

public class CarPart
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<Vehicle> VehicleCompatibility { get; set; } = new(); 
    public DateTime DateAdded { get; set; }
    public string? ImageUrl { get; set; }
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<Vehicle> Recommendations { get; set; } = new ();

	public List<ViewedCarPart> ViewedByUsers { get; set; } = new();
}
