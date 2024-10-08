using Data.Entities;

namespace Data.Repository
{
    public class UserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<User> Get()
        {
            return _context.Users.ToList();
        }

        public User? Get(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Name == username);
        }
    }
}
