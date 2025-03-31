using VroomParts.Models.User;

namespace VroomParts.Data.Repository.ApplicationUserRepository
{
    public interface IApplicationUserRepository
    {
        List<ApplicationUser> GetAll();
        ApplicationUser? GetById(string name);
        ApplicationUser CreateApplicationUser(ApplicationUser applicationUser);
        ApplicationUser UpdateApplicationUser(ApplicationUser applicationUser);
        ApplicationUser DeleteApplicationUser(ApplicationUser applicationUser);
    }
}
