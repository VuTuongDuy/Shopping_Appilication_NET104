using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class RoleServices : IRoleServices
    {
        public ShopDBContext _dbContext;
        public RoleServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddRole(Role role)
        {
            try
            {
                _dbContext.Roles.Add(role);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRole(Guid id)
        {
            try
            {
                var roleId = _dbContext.Roles.Find(id);
                _dbContext.Roles.Remove(roleId);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Role> GetAllRole()
        {
            return _dbContext.Roles.ToList();
        }

        public Role GetRoleById(Guid id)
        {
            return _dbContext.Roles.FirstOrDefault(c => c.RoleID == id);
        }

        public List<Role> GetRoleByName(string name)
        {
            return _dbContext.Roles.Where(c => c.RoleName.Contains(name)).ToList();
        }

        public bool UpdateRole(Role role)
        {
            try
            {
                var roleId = _dbContext.Roles.Find(role.RoleID);
                roleId.RoleName = role.RoleName;
                roleId.Description = role.Description;
                roleId.Status = role.Status;
                _dbContext.Roles.Update(roleId);
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
