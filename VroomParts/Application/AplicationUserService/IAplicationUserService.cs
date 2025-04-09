using VroomParts.Domain.Users;

namespace VroomParts.Application.AplicationUserService
{
	public interface IAplicationUserService
	{
		ApplicationUser GetUser(string Id);
	}
}
