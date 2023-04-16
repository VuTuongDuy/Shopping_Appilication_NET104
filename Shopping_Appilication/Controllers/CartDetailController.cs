using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Shopping_Appilication.Controllers
{
    public class CartDetailController : Controller
    {
        private readonly ILogger<CartDetailController> _logger;
        private readonly ShopDBContext _shopDBContext;
        private readonly IProductServices productServices;
        private readonly IImageServices imageServices;
        private readonly ISizeServices sizeServices;
        private readonly IUserServices userServices;
        private readonly IBillServices billServices;
        private string newMaHD = "HD01";
        public CartDetailController(ILogger<CartDetailController> logger)
        {
            _logger = logger;
            _shopDBContext = new ShopDBContext();
            productServices = new ProductServices();
            imageServices = new ImageServices();
            sizeServices = new SizeServices();
            userServices = new UserServices();
            billServices = new BillServices();
        }
        public IActionResult CartDetail()
        {
            var products = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            var viewModel = new HoaDonViewModel { CartItems = products };
            return View(viewModel);
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
            var allBills = billServices.GetAllBill();
            if (allBills != null && allBills.Count > 0)
            {
                newMaHD = "HD0" + (allBills.Count + 1).ToString();
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
                        Price = product.Price,
                        MaHD = newMaHD
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
                        Price = product.Price,
                        MaHD = newMaHD
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
        }
        [HttpPost]
        public IActionResult RemoveCartItem(Guid id)
        {
            // Lấy thông tin giỏ hàng từ session
            List<CartItem> cartItems = SessionServices.GetObjFromSession(HttpContext.Session, "Cart") as List<CartItem>;

            // Tìm kiếm sản phẩm cần xóa
            CartItem itemToRemove = cartItems.FirstOrDefault(item => item.ProductId == id);

            // Nếu sản phẩm tồn tại trong giỏ hàng, thực hiện xóa
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                // Lưu lại thông tin giỏ hàng mới vào session
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", cartItems);
            }
            // Chuyển hướng trở lại trang giỏ hàng
            return RedirectToAction("CartDetail"); 
        }
        [HttpPost]
        public IActionResult CheckoutOk(HoaDonViewModel viewModel)
        {
            // Kiểm tra xem giỏ hàng có sản phẩm nào hay không
            var cartItems = SessionServices.GetObjFromSession(HttpContext.Session, "Cart") as List<CartItem>;
            var user = new User
            {
                UserID = Guid.NewGuid(),
                UserName = viewModel.HoTen,
                DiaChi = viewModel.DiaChi,
                SoDienThoai = viewModel.SoDienThoai,
                Email = "",
                Password = "",
                RoleID = Guid.Parse("88dad5b0-513a-4404-c855-08db3450008e")
            };
            userServices.AddUser(user);

            var allBills = billServices.GetAllBill();
            if (allBills != null && allBills.Count > 0)
            {
                newMaHD = "HD0" + (allBills.Count + 1).ToString();
            }
            var bill = new Bill
            {
                Id = Guid.NewGuid(),
                MaHD = newMaHD,
                UserID = user.UserID,
                CreateDate = DateTime.Now,
                Status = viewModel.TrangThaiThanhToan,
                ToTalPrice = viewModel.TongTien
            };
            _shopDBContext.Bills.Add(bill);
            _shopDBContext.SaveChanges();
            // Thêm sản phẩm vào bảng BillDetail và cập nhật số lượng sản phẩm
            foreach (var item in cartItems)
            {
                var product = _shopDBContext.Products.Find(item.ProductId);
                var billDetail = new BillDetail
                {
                    Id = Guid.NewGuid(),
                    IdHD = bill.Id,
                    IdSP = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price * item.Quantity,
                };
                _shopDBContext.BillDetails.Add(billDetail);
                product.AvailableQuantity -= item.Quantity;
                _shopDBContext.Products.Update(product);
            }
            SessionServices.SetObjToSession(HttpContext.Session, "Cart", cartItems);
            _shopDBContext.SaveChanges();

            // Xóa giỏ hàng và chuyển hướng sang trang cảm ơn
            HttpContext.Session.Remove("Cart");
            return Content("Thanh toan thanh cong");
        }
        public IActionResult ViewBill()
        {
            List<Bill> lstBills = billServices.GetAllBill();
            return View(lstBills);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
