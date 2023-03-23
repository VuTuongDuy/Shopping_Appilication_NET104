using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class SoleServices : ISoleServices
    {
        public ShopDBContext _dbContext;
        public SoleServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddSole(Sole sole)
        {
            try
            {
                _dbContext.Soles.Add(sole);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSole(Guid id)
        {
            try
            {
                var findSole = _dbContext.Soles.Find(id);
                _dbContext.Soles.Remove(findSole);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Sole> GetAllSole()
        {
            return _dbContext.Soles.ToList();
        }

        public Sole GetSoleById(Guid id)
        {
            return _dbContext.Soles.FirstOrDefault(c => c.IdSole == id);
        }

        public List<Sole> GetSoleByName(string name)
        {
            return _dbContext.Soles.Where(c => c.Name.Contains(name)).ToList();
        }

        public bool UpdateSole(Sole sole)
        {
            try
            {
                var findSole = _dbContext.Soles.Find(sole.IdSole);
                findSole.Name = sole.Name;
                findSole.Fabric = sole.Fabric;
                findSole.Height = sole.Height;
                findSole.Status = sole.Status;
                _dbContext.Soles.Update(findSole);
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
