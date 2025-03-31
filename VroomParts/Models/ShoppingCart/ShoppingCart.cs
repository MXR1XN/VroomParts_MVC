using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VroomParts.Areas.Admin.Application.CarParts;
using VroomParts.Models.Product;
using VroomParts.Models.User;

namespace VroomParts.Models.ShoppingCart
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public Guid PartId { get; set; }
        [ForeignKey("PartId")]
        [ValidateNever]
        public CarPart? CarPart { get; set; }
        [Range(1, 50, ErrorMessage = "Please enter a value between 1 and 50")]
        public int Count { get; set; }
        public string? ApplicaiotionUserId { get; set; }
        [ForeignKey("ApplicaiotionUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [NotMapped]
        public double Price { get; set; }

    }
}
