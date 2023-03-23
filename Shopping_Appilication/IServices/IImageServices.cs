using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface IImageServices
    {
        public bool AddImage(Image image);
        public bool UpdateImage(Image image);
        public bool DeleteImage(Guid id);
        public List<Image> GetAllImage();
        public Image GetImageById(Guid id);
        public List<Image> GetImageByName(string name);
        public string GetImageUrl(Guid imageId);
    }
}
