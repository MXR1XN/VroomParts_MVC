using VroomParts.Domain.Users;

namespace VroomParts.Application.ApplicationUserService
{
	public interface IApplicationUserService
	{
		ApplicationUser GetUser(string Id);
	}
}
