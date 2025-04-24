using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using VroomParts.Domain.Cart;
using VroomParts.Domain.Orders;
using VroomParts.Domain.TrackViews;

namespace VroomParts.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public List<Order> Orders { get; set; } = [];
        public List<CartProduct> CartProducts { get; set; } = [];

		public List<ViewedCarPart> ViewedParts { get; set; } = new();
	}
}
