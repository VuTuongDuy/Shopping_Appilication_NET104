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
            string content = HttpContext.Session.GetString("Message");//Lấy data từ ssesion
            ViewData["SsesionData"] = content;
            return View();
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
        public IActionResult Create(Product p/*, [Bind]IFormFile imageFile*/)
        {
            //var x = imageFile.FileName;
            //if (imageFile != null && imageFile.Length > 0)
            //{
            //    //trỏ tới thư mục wwwroot để lát nữa thực hiện việc copy sang
            //    var path = Path.Combine(
            //        Directory.GetCurrentDirectory(), "wwwroot", "image", imageFile.FileName);
            //    using(var stream = new FileStream(path, FileMode.Create))
            //    {
            //        //thực hiện việc copy ảnh vừa chọn sang thư mục mới
            //        imageFile.CopyTo(stream);
            //    }
            //    //gán lại gtri cho description của đối tượng bằng tên file ảnh đã đc sao chép
            //    p.Description = imageFile.FileName;
            //}
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
            Product product = productServices.GetProductById(p.Id);
            //Lưu dữ liệu cũ vào Session
            HttpContext.Session.SetString("OldPrices", JsonConvert.SerializeObject(product));
            if (p.Price > product.Price)
            {
                TempData["Message"] = "Gia moi phai lon hon gia cu";
                return RedirectToAction("ShowListProduct1");
                //return BadRequest("Giá mới phải lớn hơn hoặc bằng giá cũ!");
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
            var oldPrices = HttpContext.Session.GetString("OldPrices");
            if (oldPrices == null)
            {
                return Content("Session đã bị xóa, trang web đã bị chiếm quyền kiểm soát");
            }
            var product = JsonConvert.DeserializeObject<Product>(oldPrices);

            productServices.UpdateProduct(product);

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}