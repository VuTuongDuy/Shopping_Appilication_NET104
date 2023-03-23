using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class ColorServices : IColorServices
    {
        ShopDBContext _dbContext;
        public ColorServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddColor(Color color)
        {
            try
            {
                _dbContext.Colors.Add(color);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteColor(Guid id)
        {
            try
            {
                var findColor = _dbContext.Colors.Find(id);
                _dbContext.Colors.Remove(findColor);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Color> GetAllColor()
        {
            return _dbContext.Colors.ToList();
        }

        public List<Color> GetAllColorByName(string name)
        {
            return _dbContext.Colors.Where(c => c.Name.Contains(name)).ToList();
        }

        public Color GetColorById(Guid id)
        {
            return _dbContext.Colors.FirstOrDefault(c => c.IdColor == id);
        }

        public bool UpdateColor(Color color)
        {
            try
            {
                var findColor = _dbContext.Colors.Find(color.IdColor);
                findColor.Name = color.Name;
                findColor.Status = color.Status;
                _dbContext.Colors.Update(findColor);
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
