using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface ISizeServices
    {
        public bool AddSize(Size size);
        public bool UpdateSize(Size size);
        public bool DeleteSize(Guid id);
        public List<Size> GetAllSize();
        public Size GetSizeById(Guid id);
        public List<Size> GetAllSizeByName(string name);
    }
}
