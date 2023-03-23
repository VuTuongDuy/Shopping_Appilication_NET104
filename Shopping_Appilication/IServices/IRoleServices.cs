using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface IRoleServices
    {
        public bool AddRole(Role role);
        public bool UpdateRole(Role role);
        public bool DeleteRole(Guid id);
        public List<Role> GetAllRole();
        public Role GetRoleById(Guid id);
        public List<Role> GetRoleByName(string name);
    }
}
