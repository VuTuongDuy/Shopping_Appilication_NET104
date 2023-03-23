using Shopping_Appilication.Models;

namespace Shopping_Appilication.IServices
{
    public interface IProductServices
    {
        public bool AddProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(Guid id);
        public List<Product> GetAllProducts();
        public Product GetProductById(Guid id);
        public List<Product> GetProductsByName(string name);
        public string GetImage(Guid imageId);
    }
}
