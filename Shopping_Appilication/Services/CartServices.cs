using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class CartServices : ICartServices
    {
        public ShopDBContext _dbContext;
        public CartServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddCart(Cart cart)
        {
            try
            {
                _dbContext.Carts.Add(cart);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCart(Guid id)
        {
            try
            {
                var cartId = _dbContext.Carts.Find(id);
                _dbContext.Carts.Remove(cartId);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Cart> GetAllCart()
        {
            return _dbContext.Carts.ToList();
        }

        public bool UpdateCart(Cart cart)
        {
            try
            {
                var cartId = _dbContext.Carts.FirstOrDefault(c => c.UserID == cart.UserID);
                cartId.Description = cart.Description;
                _dbContext.Carts.Update(cartId);
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
