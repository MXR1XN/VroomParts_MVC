using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VroomParts.Models.Product;

namespace VroomParts.Models.Order
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        [Required]
        public Guid OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public Guid CarPartId { get; set; }
        [ForeignKey("CarPartId")]
        [ValidateNever]
        public CarPart CarPart { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }

    }
}
