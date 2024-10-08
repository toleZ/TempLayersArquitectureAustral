using Data.Entities;
using Data.Repository;

namespace Services
{
    public class UserService
    {
        private readonly UserRepository _repo;
        public UserService(UserRepository userRepository)
        {
            _repo = userRepository;
        }
        public List<User> GetUsers()
        {
            return _repo.Get();
        }

        public User? AuthenticateUser(string username, string password)
        {
            User? userToReturn = _repo.Get(username);
            if (userToReturn is not null && userToReturn.Password == password)
                return userToReturn;
            return null;
        }
    }
}
