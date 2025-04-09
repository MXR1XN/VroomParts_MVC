namespace VroomParts.Domain.Users
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>, IReadByIdRepository<string ,ApplicationUser>
    {

    }
}
