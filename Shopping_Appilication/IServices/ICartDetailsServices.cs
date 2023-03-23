using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface ICartDetailsServices
    {
        public bool AddCartDetails(CartDetail cartDetail);
        public bool UpdateCartDetails(CartDetail cartDetail);
        public bool DeleteCartDetails(Guid id);
        public List<CartDetail> GetAllCartDetails();
        public CartDetail GetAllCartDetailsById(Guid id);
    }
}
