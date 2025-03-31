using VroomParts.Models.ShoppingCart;
using VroomParts.Models.User;

namespace VroomParts.Areas.Admin.Application.ShoppingCartService
{
	public static class ShoppingCartExtensions
	{
		public static void FillUsersDetails(this ShoppingCartVM vm, ApplicationUser user) 
		{
			vm.OrderHeader.ApplicationUser = user;
			vm.OrderHeader.Name = user.Name;
			vm.OrderHeader.PhoneNumber = user.PhoneNumber;
			vm.OrderHeader.StreetAddress = user.StreetAddress;
			vm.OrderHeader.City = user.City;
			vm.OrderHeader.State = user.State;
			vm.OrderHeader.PostalCode = user.PostalCode;
		}
	}
}
