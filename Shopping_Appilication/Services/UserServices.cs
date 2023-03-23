using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class UserServices : IUserServices
    {
        public ShopDBContext _dbContext;
        public UserServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(Guid id)
        {
            try
            {
                var userId = _dbContext.Users.Find(id);
                _dbContext.Users.Remove(userId);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<User> GetAllUser()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(Guid id)
        {
            return _dbContext.Users.FirstOrDefault(c => c.UserID == id);
        }

        public List<User> GetUsersByName(string name)
        {
            return _dbContext.Users.Where(c => c.UserName.Contains(name)).ToList();
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var userId = _dbContext.Users.Find(user.UserID);
                userId.UserName = user.UserName;
                userId.Password = user.Password;
                userId.RoleID = user.RoleID;
                userId.Status = user.Status;
                _dbContext.Users.Update(userId);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
