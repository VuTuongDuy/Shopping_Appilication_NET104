using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface IBillServices
    {
        public bool AddBill(Bill bill);
        public bool UpdateBill(Bill bill);
        public bool DeleteBill(Guid id);
        public List<Bill> GetAllBill();
        public Bill GetBillById(Guid id);
        //public List<Bill> GetBillByName(string name);
    }
}
