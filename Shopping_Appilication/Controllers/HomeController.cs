using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System;
using System.Diagnostics;

namespace Shopping_Appilication.Controllers 
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices productServices;//Interface
        private readonly IImageServices imageServices;
        private readonly IUserServices userServices;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productServices = new ProductServices();//class 
            imageServices = new ImageServices();
            userServices = new UserServices();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var ProductDelete = HttpContext.Session.GetString("Product");
            if (ProductDelete == null)
            {
                return Content("Lam gi co ma hien thi");
            }
            var product = JsonConvert.DeserializeObject<Product>(ProductDelete);
            return View(new List<Product> { product });
        }
        public IActionResult Redirect()
        {
            return RedirectToAction("Index");
        }
        public IActionResult ShowListProduct1()
        {
            List<Product> products = productServices.GetAllProducts();
            return View(products);
        }
        public IActionResult Details(Guid id)
        {
            ShopDBContext dbContext = new ShopDBContext();
            var product = dbContext.Products.Find(id);
            var detailsShoes = from a in dbContext.Products
                               join b in dbContext.Images on a.IdImage equals b.IdImage
                               where a.Id == id
                               select new
                               {
                                   Product = a,
                                   Image1 = b.Image1,
                                   Image2 = b.Image2,
                                   Image3 = b.Image3,
                                   Image4 = b.Image4,
                               };
            ViewBag.Image1 = detailsShoes.FirstOrDefault().Image1;
            ViewBag.Image2 = detailsShoes.FirstOrDefault().Image2;
            ViewBag.Image3 = detailsShoes.FirstOrDefault().Image3;
            ViewBag.Image4 = detailsShoes.FirstOrDefault().Image4;
            return View(product);
        }
        public IActionResult Create()
        {
            using (ShopDBContext shopDBContext = new ShopDBContext())
            {
                var images = shopDBContext.Images.ToList();
                SelectList selectListImage = new SelectList(images, "IdImage", "Name");
                var sizes = shopDBContext.Sizes.ToList();
                SelectList selectListsSize = new SelectList(sizes, "IdSize", "Name");
                ViewBag.ImageList = selectListImage;
                ViewBag.SizeList = selectListsSize;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (productServices.GetAllProducts().Any(c => c.Name == p.Name) || productServices.GetAllProducts().Any(c => c.Supplier == p.Supplier))
            {
                return Content("Ten sp va nha sx da ton tai");
            }else
            if (productServices.AddProduct(p))
            {
                return RedirectToAction("ShowListProduct1");
            }
            else return BadRequest();
        }
        [HttpGet]
        public IActionResult Edit(Guid id) // Khi ấn vào Create thì hiển thị View
        {
            using (ShopDBContext shopDBContext = new ShopDBContext())
            {
                var images = shopDBContext.Images.ToList();
                SelectList selectListImage = new SelectList(images, "IdImage", "Name");
                var sizes = shopDBContext.Sizes.ToList();
                SelectList selectListsSize = new SelectList(sizes, "IdSize", "Name");
                ViewBag.ImageList = selectListImage;
                ViewBag.SizeList = selectListsSize;
            }
            // Lấy Product từ database dựa theo id truyền vào từ route
            Product p = productServices.GetProductById(id);
            return View(p);
        }

        public IActionResult Edit(Product p) // Thực hiện việc Tạo mới
        {
            //Product product = productServices.GetProductById(p.Id);
            ////Lưu dữ liệu cũ vào Session
            //HttpContext.Session.SetString("OldPrices", JsonConvert.SerializeObject(product));
            if (productServices.GetAllProducts().Any(c => c.Name == p.Name) || productServices.GetAllProducts().Any(c => c.Supplier == p.Supplier))
            {
                return Content("Ten sp va nha sx da ton tai");
            }
            else
            {
                if (productServices.UpdateProduct(p))
                {
                    return RedirectToAction("ShowListProduct1");
                }
                else return BadRequest();
            }
        }
        public IActionResult Delete(Guid id)
        {
            var ProductDelete = productServices.GetProductById(id);
            HttpContext.Session.SetString("Product", JsonConvert.SerializeObject(ProductDelete));
            if (productServices.DeleteProduct(id))
            {
                return RedirectToAction("ShowListProduct1");
            }
            else return BadRequest();
        }
        public IActionResult Products()
        {
            List<Product> lstProduct = productServices.GetAllProducts();
            foreach (var product in lstProduct)
            {
                string imageUrl = imageServices.GetImageUrl((Guid)product.IdImage);
                product.ImageUrl = imageUrl;
            }
            return View(lstProduct);
        }
        public IActionResult ShowListFromSession()
        {
            var oldPrices = HttpContext.Session.GetString("OldPrices");
            if (oldPrices == null)
            {
                return Content("Session đã bị xóa, trang web đã bị chiếm quyền kiểm soát");
            }
            var products = JsonConvert.DeserializeObject<Product>(oldPrices);
            return View(new List<Product> { products });
        }
        public IActionResult RollbackProduct(Guid id)
        {
            var ProductDelete = HttpContext.Session.GetString("Product");
            if (productServices == null)
            {
                return Content("Co cai nit");
            }
            var product = JsonConvert.DeserializeObject<Product>(ProductDelete);
            productServices.AddProduct(product);
            HttpContext.Session.Remove("Product");
            return RedirectToAction("ShowListProduct1");
        }

        public IActionResult Search(string name)
        {
            try
            {
                var products = productServices.GetProductsByName(name);
                foreach (var product in products)
                {
                    string imageUrl = imageServices.GetImageUrl((Guid)product.IdImage);
                    product.ImageUrl = imageUrl;
                }
                ViewBag.Name = name;
                return View(products);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult TransferData() // Đẩy dữ liệu qua các View
        {
            // Để truyền đc data sang view thì ngoài cách trực tiếp 1 object model ta có thể sử dụng các cách sau:
            /* 
             * 1. Sử dụng ViewBag: Dữ liệu trong ViewBag là dl dynamic. Không cần khởi tạo thành phần ViewBag mà đặt tên luôn
             * 
             * */
            int[] marks = { 1, 2, 3, 4, 5, 6, 7 };
            List<string> characterName = new List<string>() { "Paris", "Tokyo", "Berlin", "Moskow", "Stockhoml", "Berg", "Rome", "Nairobi", "Seoul" };
            ViewBag.Marks = marks;// Gán dữ liệu vào ViewBag
            ViewBag.Marks1 = characterName;// Gán dữ liệu vào ViewBag
            /* 
             * Sử dụng ViewData: Data sẽ đc truyền tải dưới dạng Key-Value nhưng dl lại ở dạng Generic
             */
            ViewData["name"] = characterName;
            /*
             * Sử dụng Session (Phiên làm việc), cơ chế key - value
             */
            string mess = "Hungry, Crazy";
            HttpContext.Session.SetString("Message", mess);//Truyền data vào ssesion
            string content = HttpContext.Session.GetString("Message");//Lấy data từ ssesion
            ViewData["SsesionData"] = content;
            return View();
        }
        public IActionResult SearchSP(string sanpham)
        {
            var sanPham = productServices.GetProductsByName(sanpham);
            if (sanPham == null)
            {
                return Content("Ko tim thay");
            }
            return View(sanPham);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}