using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface IUserServices
    {
        public bool AddUser(User user);
        public bool UpdateUser(User user);
        public bool DeleteUser(Guid id);
        public List<User> GetAllUser();
        public User GetUserById(Guid id);
        public List<User> GetUsersByName(string name);
    }
}
