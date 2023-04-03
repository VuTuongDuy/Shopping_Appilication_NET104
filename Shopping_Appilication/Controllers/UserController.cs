using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;

namespace Shopping_Appilication.Controllers
{
    public class UserController:Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices userServices;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            userServices = new UserServices();
        }
        public IActionResult GetAllUser()
        {
            List<User> lstUser = userServices.GetAllUser();
            return View(lstUser);
        }
        public IActionResult Create()
        {
            using (ShopDBContext shopDBContext = new ShopDBContext())
            {
                var role = shopDBContext.Roles.ToList();
                SelectList selectListsRole = new SelectList(role, "RoleID", "RoleName");
                ViewBag.ListRole = selectListsRole;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (userServices.AddUser(user))
            {
                return RedirectToAction("GetAllUser");
            }
            else return BadRequest();
        }
        public IActionResult Details(Guid id)
        {
            ShopDBContext dbContext = new ShopDBContext();
            var user = dbContext.Users.Find(id);
            return View(user);
        }
        [HttpGet]
        public IActionResult Edit(Guid id) // Khi ấn vào Create thì hiển thị View
        {
            using (ShopDBContext shopDBContext = new ShopDBContext())
            {
                var role = shopDBContext.Roles.ToList();
                SelectList selectListsRole = new SelectList(role, "RoleID", "RoleName");
                ViewBag.ListRole = selectListsRole;
            }
            // Lấy Product từ database dựa theo id truyền vào từ route
            User user = userServices.GetUserById(id);
            return View(user);
        }

        public IActionResult Edit(User user) // Thực hiện việc Tạo mới
        {
            if (userServices.UpdateUser(user))
            {
                return RedirectToAction("GetAllUser");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (userServices.DeleteUser(id))
            {
                return RedirectToAction("GetAllUser");
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
