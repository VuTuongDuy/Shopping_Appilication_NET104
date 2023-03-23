using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class BillDetailsServices : IBillDetailsServices
    {
        public ShopDBContext _dbContext;
        public BillDetailsServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddBillDetails(BillDetail billDetail)
        {
            try
            {
                _dbContext.BillDetails.Add(billDetail);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBillDetails(Guid id)
        {
            try
            {
                var billDetailsId = _dbContext.BillDetails.Find(id);
                _dbContext.BillDetails.Remove(billDetailsId);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BillDetail> GetAllBillDetails()
        {
            return _dbContext.BillDetails.ToList();
        }

        public BillDetail GetBillDetailsById(Guid id)
        {
            return _dbContext.BillDetails.FirstOrDefault(c => c.Id == id);
        }

        public bool UpdateBillDetails(BillDetail billDetail)
        {
            try
            {
                var billDetailsId = _dbContext.BillDetails.FirstOrDefault(c => c.Id == billDetail.Id);
                billDetailsId.IdHD = billDetail.IdHD;
                billDetailsId.IdSP = billDetail.IdSP;
                billDetailsId.Price = billDetail.Price;
                billDetailsId.Quantity = billDetail.Quantity;
                _dbContext.BillDetails.Update(billDetailsId);
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
