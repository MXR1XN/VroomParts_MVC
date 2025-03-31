using VroomParts.Models.User;

namespace VroomParts.Data.Repository.ApplicationUserRepository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDBContext _context;

        public ApplicationUserRepository(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public ApplicationUser CreateApplicationUser(ApplicationUser applicationUser)
        {
            _context.Add(applicationUser);
            _context.SaveChanges();
            return applicationUser;
        }

        public ApplicationUser DeleteApplicationUser(ApplicationUser applicationUser)
        {
            _context.Remove(applicationUser);
            _context.SaveChanges();
            return applicationUser;
        }

        public List<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers.ToList();
        }

        public ApplicationUser? GetById(string Id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(j => j.Id == Id);
            return user;
        }

        public ApplicationUser UpdateApplicationUser(ApplicationUser applicationUser)
        {
            _context.Update(applicationUser);
            _context.SaveChanges();
            return applicationUser;
        }
    }
}
