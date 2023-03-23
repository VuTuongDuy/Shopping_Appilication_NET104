using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Size = Shopping_Appilication.Models.Size;

namespace Shopping_Appilication.Services
{
    public class SizeServices : ISizeServices
    {
        ShopDBContext _dbContext;
        public SizeServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddSize(Size size)
        {
            try
            {
                _dbContext.Sizes.Add(size);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSize(Guid id)
        {
            try
            {
                var findSize = _dbContext.Sizes.Find(id);
                _dbContext.Sizes.Remove(findSize);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Size> GetAllSize()
        {
            return _dbContext.Sizes.ToList();
        }

        public List<Size> GetAllSizeByName(string name)
        {
            return _dbContext.Sizes.Where(c => c.Name.Contains(name)).ToList();
        }

        public Size GetSizeById(Guid id)
        {
            return _dbContext.Sizes.FirstOrDefault(c => c.IdSize == id);
        }

        public bool UpdateSize(Size size)
        {
            try
            {
                var findSize = _dbContext.Sizes.Find(size.IdSize);
                findSize.Name = size.Name;
                findSize.Status = size.Status;
                _dbContext.Sizes.Update(findSize);
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
