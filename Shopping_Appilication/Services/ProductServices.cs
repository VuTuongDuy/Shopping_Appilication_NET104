using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;

namespace Shopping_Appilication.Services
{
    public class ProductServices : IProductServices
    {
        ShopDBContext _dbContext;
        public ProductServices()
        {
            _dbContext = new ShopDBContext();
        }
        public bool AddProduct(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                var product = _dbContext.Products.Find(id);
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
            //return _dbContext.Products.Where(c => c.Status < 3).ToList();
        }

        public Product GetProductById(Guid id)
        {
            return _dbContext.Products.FirstOrDefault(c => c.Id == id);// tìm ra thằng đầu tiên trong những thằng giống  nhau
            //return _dbContext.Products.SingleOrDefault(c => c.Id == id);// nếu có nhiều phân tử giống nhau thì sẽ tìm ra thằng đầu tiên và những thằng còn lại quăng sang exceptison
        }

        public List<Product> GetProductsByName(string name)
        {
            return _dbContext.Products.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                var productID = _dbContext.Products.Find(product.Id);
                productID.Name = product.Name;
                productID.Description = product.Description;
                productID.Price = product.Price;
                productID.Supplier = product.Supplier;
                productID.AvailableQuantity = product.AvailableQuantity;
                productID.Status = product.Status;
                productID.IdColor = product.IdColor;
                productID.IdSize = product.IdSize;
                productID.IdSole = product.IdSole;
                productID.IdImage = product.IdImage;
                _dbContext.Products.Update(productID);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string GetImage(Guid imageId)
        {
            var image = _dbContext.Images.FirstOrDefault(c => c.IdImage == imageId);
            if (image != null)
            {
                return image.Image1;
            }
            return "Error";
        }
    }
}
