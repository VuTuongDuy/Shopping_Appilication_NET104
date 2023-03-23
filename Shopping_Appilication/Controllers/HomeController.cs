﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;

namespace Shopping_Appilication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices productServices;//Interface
        private readonly IImageServices imageServices;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productServices = new ProductServices();//class 
            imageServices = new ImageServices();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Redirect()
        {
            return RedirectToAction("Index");
        }
        public IActionResult Show()
        {
            Product product = new Product() { 
                Id = Guid.NewGuid(),
                Name = "Nike Air 5",
                AvailableQuantity = 1,
                Supplier = "Nike",
                Description = "Black shoes",
                Price = 200000,
                Status = 0
            };
            return View(product);
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
                SelectList selectList = new SelectList(images, "IdImage", "Name");
                ViewBag.ImageList = selectList;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
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
                SelectList selectList = new SelectList(images, "IdImage", "Name");
                ViewBag.ImageList = selectList;
            }
            // Lấy Product từ database dựa theo id truyền vào từ route
            Product p = productServices.GetProductById(id);
            return View(p);
        }

        public IActionResult Edit(Product p) // Thực hiện việc Tạo mới
        {
            Product product = productServices.GetProductById(p.Id);
            if(p.Price >= product.Price)
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
                //string imageUrl = null;
                //if (product.IdImage.HasValue)
                //{
                //    imageUrl = imageServices.GetImageUrl(product.IdImage.Value);
                //}
                //product.ImageUrl = imageUrl;
            }
            return View(lstProduct);
        }
        public IActionResult Admin()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}