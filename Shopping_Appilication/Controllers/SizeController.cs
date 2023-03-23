using Microsoft.AspNetCore.Mvc;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;

namespace Shopping_Appilication.Controllers
{
    public class SizeController:Controller
    {
        private readonly ILogger<SizeController> _logger;
        private readonly ISizeServices sizeServices;
        public SizeController(ILogger<SizeController> logger)
        {
            _logger = logger;
            sizeServices = new SizeServices();
        }
        public IActionResult GetAllSize()
        {
            List<Size> lstSize = sizeServices.GetAllSize();
            return View(lstSize);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Size size)
        {
            if (sizeServices.AddSize(size))
            {
                return RedirectToAction("GetAllSize");
            }
            else return BadRequest();
        }
        public IActionResult Details(Guid id)
        {
            ShopDBContext dbContext = new ShopDBContext();
            var size = dbContext.Sizes.Find(id);
            return View(size);
        }
        [HttpGet]
        public IActionResult Edit(Guid id) // Khi ấn vào Create thì hiển thị View
        {
            // Lấy Product từ database dựa theo id truyền vào từ route
            Size size = sizeServices.GetSizeById(id);
            return View(size);
        }

        public IActionResult Edit(Size size) // Thực hiện việc Tạo mới
        {
            if (sizeServices.UpdateSize(size))
            {
                return RedirectToAction("GetAllSize");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (sizeServices.DeleteSize(id))
            {
                return RedirectToAction("GetAllSize");
            }
            else return BadRequest();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
