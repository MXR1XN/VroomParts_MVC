using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.Net;
using VroomParts.Domain.Cart;
using VroomParts.Domain.Orders;

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
    }
}
