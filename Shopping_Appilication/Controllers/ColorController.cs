using Microsoft.AspNetCore.Mvc;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;

namespace Shopping_Appilication.Controllers
{
    public class ColorController:Controller
    {
        private readonly ILogger<ColorController> _logger;
        private readonly IColorServices colorServices;
        public ColorController(ILogger<ColorController> logger)
        {
            _logger = logger;
            colorServices = new ColorServices();
        }
        public IActionResult GetAllColor()
        {
            List<Color> lstColor = colorServices.GetAllColor();
            return View(lstColor);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Color color)
        {
            if (colorServices.AddColor(color))
            {
                return RedirectToAction("GetAllColor");
            }
            else return BadRequest();
        }
        public IActionResult Details(Guid id)
        {
            ShopDBContext dbContext = new ShopDBContext();
            var color = dbContext.Colors.Find(id);
            return View(color);
        }
        [HttpGet]
        public IActionResult Edit(Guid id) // Khi ấn vào Create thì hiển thị View
        {
            // Lấy Product từ database dựa theo id truyền vào từ route
            Color color = colorServices.GetColorById(id);
            return View(color);
        }

        public IActionResult Edit(Color color) // Thực hiện việc Tạo mới
        {
            if (colorServices.UpdateColor(color))
            {
                return RedirectToAction("GetAllColor");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (colorServices.DeleteColor(id))
            {
                return RedirectToAction("GetAllColor");
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
