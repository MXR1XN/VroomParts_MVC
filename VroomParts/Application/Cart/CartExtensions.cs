using VroomParts.Domain.Cart;

namespace VroomParts.Application.Cart
{
    public static class CartExtensions
    {
        public static CartProductDTO ToDto(this CartProduct vm)
        {
            return new CartProductDTO()
            {
                Id = vm.CarPart!.Id,
                Name = vm.CarPart.Name,
                Price = vm.CarPart.Price,
                Description = vm.CarPart.Description,
                Count = vm.Count,
                Category = vm.CarPart.Category!.Name,
                ImageUrl = vm.CarPart.ImageUrl
            };
        }
    }
}
