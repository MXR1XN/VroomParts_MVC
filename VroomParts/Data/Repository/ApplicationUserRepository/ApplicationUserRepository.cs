using VroomParts.Domain.Users;

namespace VroomParts.Data.Repository.ApplicationUserRepository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDBContext _context;

        public ApplicationUserRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public ApplicationUser? Find(string id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u=> u.Id == id);
        }
    }
}
