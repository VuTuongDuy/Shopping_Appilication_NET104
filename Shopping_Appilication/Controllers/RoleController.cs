using Microsoft.AspNetCore.Mvc;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;

namespace Shopping_Appilication.Controllers
{
    public class RoleController:Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleServices roleServices;
        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
            roleServices = new RoleServices();
        }
        public IActionResult GetAllRole()
        {
            List<Role> lstRole = roleServices.GetAllRole();
            return View(lstRole);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (roleServices.AddRole(role))
            {
                return RedirectToAction("GetAllRole");
            }
            else return BadRequest();
        }
        public IActionResult Details(Guid id)
        {
            ShopDBContext dbContext = new ShopDBContext();
            var role = dbContext.Roles.Find(id);
            return View(role);
        }
        [HttpGet]
        public IActionResult Edit(Guid id) // Khi ấn vào Create thì hiển thị View
        {
            // Lấy Product từ database dựa theo id truyền vào từ route
            Role role = roleServices.GetRoleById(id);
            return View(role);
        }

        public IActionResult Edit(Role role) // Thực hiện việc Tạo mới
        {

            if (roleServices.UpdateRole(role))
            {
                return RedirectToAction("GetAllRole");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (roleServices.DeleteRole(id))
            {
                return RedirectToAction("GetAllRole");
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
