using VroomParts.Domain.Users;

namespace VroomParts.Application.AplicationUserService
{
	public class AplicationUserService : IAplicationUserService
	{
		private readonly IApplicationUserRepository _userRepository;

		public AplicationUserService(IApplicationUserRepository applicationUser) 
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
