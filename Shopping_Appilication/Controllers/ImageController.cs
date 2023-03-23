using Microsoft.AspNetCore.Mvc;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;

namespace Shopping_Appilication.Controllers
{
    public class ImageController:Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageServices imageServices;
        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
            imageServices = new ImageServices();
        }
        public IActionResult GetAllImage()
        {
            List<Image> lstImage = imageServices.GetAllImage();
            return View(lstImage);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Image image)
        {
            if (imageServices.AddImage(image))
            {
                return RedirectToAction("GetAllImage");
            }
            else return BadRequest();
        }
        public IActionResult Details(Guid id)
        {
            ShopDBContext dbContext = new ShopDBContext();
            var image = dbContext.Images.Find(id);
            return View(image);
        }
        [HttpGet]
        public IActionResult Edit(Guid id) // Khi ấn vào Create thì hiển thị View
        {
            // Lấy Product từ database dựa theo id truyền vào từ route
            Image image = imageServices.GetImageById(id);
            return View(image);
        }

        public IActionResult Edit(Image image) // Thực hiện việc Tạo mới
        {
            if (imageServices.UpdateImage(image))
            {
                return RedirectToAction("GetAllImage");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (imageServices.DeleteImage(id))
            {
                return RedirectToAction("GetAllImage");
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
