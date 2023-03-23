using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class CartDetailsServices : ICartDetailsServices
    {
        public ShopDBContext _dbContext;
        public CartDetailsServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddCartDetails(CartDetail cartDetail)
        {
            try
            {
                _dbContext.CartDetails.Add(cartDetail);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCartDetails(Guid id)
        {
            try
            {
                var cartDetailsId = _dbContext.CartDetails.Find(id);
                _dbContext.CartDetails.Remove(cartDetailsId);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CartDetail> GetAllCartDetails()
        {
            return _dbContext.CartDetails.ToList();
        }

        public CartDetail GetAllCartDetailsById(Guid id)
        {
            return _dbContext.CartDetails.FirstOrDefault(c => c.Id == id);
        }

        public bool UpdateCartDetails(CartDetail cartDetail)
        {
            try
            {
                var cartDetailsId = _dbContext.CartDetails.FirstOrDefault(c => c.Id == cartDetail.Id);
                cartDetailsId.UserID = cartDetail.UserID;
                cartDetailsId.IDSP = cartDetail.IDSP;
                cartDetailsId.Quantity = cartDetail.Quantity;
                _dbContext.CartDetails.Update(cartDetailsId);
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
