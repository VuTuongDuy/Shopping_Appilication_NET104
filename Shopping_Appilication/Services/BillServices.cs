using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class BillServices : IBillServices
    {
        public ShopDBContext _dbContext;
        public BillServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddBill(Bill bill)
        {
            try
            {
                _dbContext.Bills.Add(bill);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBill(Guid id)
        {
            try
            {
                var billId = _dbContext.Bills.FirstOrDefault(c => c.Id == id);
                _dbContext.Bills.Remove(billId);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Bill> GetAllBill()
        {
            return _dbContext.Bills.ToList();
        }

        public Bill GetBillById(Guid id)
        {
            return _dbContext.Bills.FirstOrDefault(c => c.Id == id);
        }

        //public List<Bill> GetBillByName(string name)
        //{
        //    return false;
        //}

        public bool UpdateBill(Bill bill)
        {
            try
            {
                var billId = _dbContext.Bills.FirstOrDefault(c => c.Id == bill.Id);
                billId.CreateDate = bill.CreateDate;
                billId.UserID = bill.UserID;
                billId.Status = bill.Status;
                _dbContext.Bills.Update(billId);
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
