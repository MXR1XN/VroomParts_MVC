using VroomParts.Domain.Cart;
using VroomParts.Domain.Users;
using VroomParts.Utility;

namespace VroomParts.Application.ApplicationUserService
{
	public class ApplicationUserService : IApplicationUserService
	{
		private readonly IApplicationUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationUserService(IApplicationUserRepository applicationUser, IHttpContextAccessor contextAccessor) 
		{
			_userRepository = applicationUser;
			_contextAccessor = contextAccessor;
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

		public bool IsAdministrator() 
		{
			if (_contextAccessor.HttpContext.User.IsInRole(StaticDetail.Role_Admin)) 
			{
				return true;
			}
			return false;
		}
	}
}
