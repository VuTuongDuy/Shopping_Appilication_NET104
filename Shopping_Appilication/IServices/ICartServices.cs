using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface ICartServices
    {
        public bool AddCart(Cart cart);
        public bool UpdateCart(Cart cart);
        public bool DeleteCart(Guid id);
        public List<Cart> GetAllCart();
    }
}
