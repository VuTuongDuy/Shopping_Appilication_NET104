using Microsoft.EntityFrameworkCore;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class ImageServices : IImageServices
    {
        ShopDBContext _dbContext;
        public ImageServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddImage(Image image)
        {
            try
            {
                _dbContext.Images.Add(image);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteImage(Guid id)
        {
            try
            {
                var findImg = _dbContext.Images.Find(id);
                _dbContext.Images.Remove(findImg);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Image> GetAllImage()
        {
            return _dbContext.Images.ToList();
        }

        public Image GetImageById(Guid id)
        {
            return _dbContext.Images.FirstOrDefault(c => c.IdImage == id);
        }

        public List<Image> GetImageByName(string name)
        {
            return _dbContext.Images.Where(c => c.Name.Contains(name)).ToList();
        }

        public bool UpdateImage(Image image)
        {
            try
            {
                var findImg = _dbContext.Images.Find(image.IdImage);
                findImg.Name = image.Name;
                findImg.Image1 = image.Image1;
                findImg.Image2 = image.Image2;
                findImg.Image3 = image.Image3;
                findImg.Image4 = image.Image4;
                findImg.Status = image.Status;
                _dbContext.Images.Update(findImg);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string GetImageUrl(Guid imageId)
        {
            var img = _dbContext.Images.FirstOrDefault(c => c.IdImage == imageId);
            if (img != null)
            {
                return img.Image1;
            }
            else
            {
                return "Error";
            }
        }
    }
}
