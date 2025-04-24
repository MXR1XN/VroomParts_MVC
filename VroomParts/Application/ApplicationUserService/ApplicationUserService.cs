using VroomParts.Domain.Cart;
using VroomParts.Domain.Users;

namespace VroomParts.Application.ApplicationUserService
{
	public class ApplicationUserService : IApplicationUserService
	{
		private readonly IApplicationUserRepository _userRepository;

		public ApplicationUserService(IApplicationUserRepository applicationUser) 
		{
			_userRepository = applicationUser;
		}
		public ApplicationUser GetUser(string Id)
		{
			var user = _userRepository.Find(Id);
			if (user is null) 
			{
				throw new Exception("Not found");
			}
			return user;
		}
	}
}
