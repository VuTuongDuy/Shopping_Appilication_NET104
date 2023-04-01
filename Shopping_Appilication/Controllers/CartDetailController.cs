using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace Shopping_Appilication.Controllers
{
    public class CartDetailController : Controller
    {
        private readonly ILogger<CartDetailController> _logger;
        private readonly ShopDBContext _shopDBContext;
        private readonly IProductServices productServices;
        private readonly IImageServices imageServices;
        private readonly ISizeServices sizeServices;
        public CartDetailController(ILogger<CartDetailController> logger)
        {
            _logger = logger;
            _shopDBContext = new ShopDBContext();
            productServices = new ProductServices();
            imageServices = new ImageServices();
            sizeServices = new SizeServices();
        }
        public IActionResult CartDetail()
        {
            var products = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            return View(products);
        }
        public IActionResult AddToCart(Guid id, string size)
        {
            // Lấy dữ liệu sản phẩm
            var product = productServices.GetProductById(id);

            // Kiểm tra sản phẩm có tồn tại không
            if (product == null)
            {
                return NotFound();
            }

            // Lấy dữ liệu giỏ hàng từ Session
            var cartItems = SessionServices.GetObjFromSession(HttpContext.Session, "Cart") as List<CartItem>;
            //Lấy ảnh của sp
            List<Product> lstProduct = productServices.GetAllProducts();
            foreach (var product1 in lstProduct)
            {
                string imageUrl = imageServices.GetImageUrl((Guid)product1.IdImage);
                product1.ImageUrl = imageUrl;
            }
            // Kiểm tra giỏ hàng đã tồn tại chưa
            if (cartItems == null)
            {
                // Nếu chưa có, tạo mới giỏ hàng với sản phẩm đó
                cartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        ProductId = product.Id,
                        Size = size,
                        Quantity = 1,
                        ProductImage = product.ImageUrl,
                        ProductName = product.Name,
                        Description = product.Description,
                        Price = product.Price
                    }
                };

                // Lưu giỏ hàng vào Session
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", cartItems);
            }
            else
            {
                // Nếu giỏ hàng đã tồn tại, kiểm tra sản phẩm đó đã có trong giỏ hàng chưa
                var cartItem = cartItems.FirstOrDefault(ci => ci.ProductId == product.Id && ci.Size == size);

                if (cartItem == null)
                {
                    // Nếu chưa có, thêm sản phẩm vào giỏ hàng với số lượng là 1
                    cartItems.Add(new CartItem
                    {
                        ProductId = product.Id,
                        Size = size,
                        Quantity = 1,
                        ProductImage = product.ImageUrl,
                        ProductName = product.Name,
                        Description = product.Description,
                        Price = product.Price
                    });
                }
                else
                {
                    // Nếu sản phẩm đã có trong giỏ hàng, tăng số lượng lên 1
                    cartItem.Quantity++;
                }

                // Lưu giỏ hàng vào Session
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", cartItems);
            }

            // Chuyển hướng đến trang giỏ hàng
            return RedirectToAction("CartDetail");
            ////Lấy đc dữ liệu sản mới
            //var product = productServices.GetProductById(id);
            ////lấy dữ liệu từ cart(trong Session)
            //var products = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            ////Thêm list này vào ssesion
            //List<Product> lstProduct = productServices.GetAllProducts();
            //foreach (var product1 in lstProduct)
            //{
            //    string imageUrl = imageServices.GetImageUrl((Guid)product1.IdImage);
            //    product1.ImageUrl = imageUrl;
            //}
            ////Kiểm tra xem List dl đó có phần tử chưa
            //if (products.Count == 0)
            //{
            //    products.Add(product); // Nếu ko có sp nào thì add nó vào
            //    //Sau đó gán lại giá trị vào trong Session
            //    SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            //}
            //else
            //{
            //    if (SessionServices.CheckObjInList(id, products))
            //    {
            //        return Content("List đã chứa sản phẩm này, bạn định ăn cắp ư");//thực hiện update thêm số lượng
            //    }
            //    else
            //    {
            //        products.Add(product); // Nếu chưa có sp đó 
            //        //Sau đó gán lại giá trị vào trong Session
            //        SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            //    }
            //}
            //return RedirectToAction("CartDetail");
        }
        //public IActionResult GetCartBySsesion()
        //{
        //    Cart cart = HttpContext.Session.GetObject<Cart>("Cart");
        //    return View(cart);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
