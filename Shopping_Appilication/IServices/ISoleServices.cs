using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface ISoleServices
    {
        public bool AddSole(Sole sole);
        public bool UpdateSole(Sole sole);
        public bool DeleteSole(Guid id);
        public List<Sole> GetAllSole();
        public Sole GetSoleById(Guid id);
        public List<Sole> GetSoleByName(string name);
    }
}
