using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface IBillDetailsServices
    {
        public bool AddBillDetails(BillDetail billDetail);
        public bool UpdateBillDetails(BillDetail billDetail);
        public bool DeleteBillDetails(Guid id);
        public List<BillDetail> GetAllBillDetails();
        public BillDetail GetBillDetailsById(Guid id);
    }
}
