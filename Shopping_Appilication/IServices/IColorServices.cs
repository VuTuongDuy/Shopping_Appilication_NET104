using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface IColorServices
    {
        public bool AddColor(Color color);
        public bool UpdateColor(Color color);
        public bool DeleteColor(Guid id);
        public List<Color> GetAllColor();
        public Color GetColorById(Guid id);
        public List<Color> GetAllColorByName(string name);
    }
}
