using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping_Appilication.IServices;
using Shopping_Appilication.Models;
using Shopping_Appilication.Services;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;

namespace Shopping_Appilication.Controllers
{
    public class UserController:Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices userServices;
        private readonly IConfiguration _configuration;
        private ShopDBContext _context;
        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            userServices = new UserServices();
            _configuration = configuration;
            _context = new ShopDBContext();
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
        public IActionResult SignUp1()
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
        public IActionResult SignUp1(User user, string ConfirmPassword)
        {
            if (user.Password != ConfirmPassword)
            {
                return View();
            }else
            if (userServices.GetAllUser().Any(c => c.UserName == user.UserName))
            {
                return Json(new { success = false, message = "Tên đăng nhập đã tồn tại" });
            }
            else
            if (userServices.AddUser(user))
            {
                TempData["UserName"] = user.UserName;
                TempData["Password"] = user.Password;
                TempData["SignUpSuccess"] = "Đăng ký tài khoản thành công!";
                return Json(new { success = true, redirectUrl = Url.Action("Login", "User") });
            }
            else return BadRequest();
        }
        public IActionResult Login()
        {
            ViewBag.SignUpSuccess = TempData["SignUpSuccess"];
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var loggedInUser = userServices.GetAllUser().FirstOrDefault(c => c.UserName == user.UserName && c.Password == user.Password);
            if (loggedInUser != null)
            {
                HttpContext.Session.SetString("UserId", JsonConvert.SerializeObject(loggedInUser.UserID.ToString()));
                HttpContext.Session.SetString("UserName", JsonConvert.SerializeObject(loggedInUser.UserName));
                //HttpContext.Session.SetString("UserId", loggedInUser.UserID.ToString());
                //HttpContext.Session.SetString("UserName", loggedInUser.UserName);
                TempData["SignUpSuccess"] = "Đăng nhập thành công!";
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng nhập đúng thông tin tài khoản" });
            }
        }
        //public IActionResult Login()
        //{
        //    ViewBag.SignUpSuccess = TempData["SignUpSuccess"];
        //    return View();
        //}
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            try
            {
                var nhanVien = userServices.GetAllUser().FirstOrDefault(c => c.Email == Email);
                if (nhanVien == null)
                {
                    return Json(new { success = false, message = "Email không tồn tại" });
                }
                var randomCode = GenerateToken();
                var subject = "Mã xác nhận đổi mật khẩu tài khoản Nike&08Pt";
                var body = $"Mã xác nhận của bạn là: {randomCode}";
                // Gửi email
                GuiMail(nhanVien.Email, subject, body);
                // Cập nhật mã xác nhận mới cho nhân viên
                nhanVien.ResetPasswordToken = randomCode;
                userServices.UpdateUser(nhanVien);
                TempData["SignUpSuccess"] = "Mã xác nhận đã được gửi, vui lòng kiểm tra hòm thư!";
                //return Json(new { success = true, message = "Gửi mail thành công" });
                return RedirectToAction("VerificationCode");
            }
            catch
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi gửi mail. Hãy thử lại sau" });
            }
        }
        public string GenerateToken()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        public void GuiMail(string Email, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress("vuduy10a7@gmail.com");
            message.To.Add(Email);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("vuduy10a7@gmail.com", "yzcgeyulyergpmjw");

            // Thêm lệnh ghi log
            smtpClient.SendCompleted += (sender, args) =>
            {
                if (args.Error != null)
                {
                    // Ghi log lỗi
                    Console.WriteLine($"Lỗi gửi email: {args.Error.Message}");
                }
                else if (args.Cancelled)
                {
                    // Ghi log hủy bỏ gửi email
                    Console.WriteLine("Gửi email bị hủy bỏ.");
                }
                else
                {
                    // Ghi log gửi email thành công
                    Console.WriteLine("Gửi email thành công.");
                }
            };
            smtpClient.Send(message);
        }
        public IActionResult VerificationCode()
        {
            ViewBag.SignUpSuccess = TempData["SignUpSuccess"];
            return View();
        }
        [HttpPost]
        public IActionResult VerificationCode(string CodeXacThuc)
        {
            var user = userServices.GetAllUser().FirstOrDefault(c => c.ResetPasswordToken == CodeXacThuc);
            if (user == null)
            {
                // Nếu không tìm thấy user với mã xác thực này, hiển thị thông báo lỗi.
                TempData["ErrorMessage"] = "Mã xác thực không hợp lệ.";
                return View();
            }
            // Kiểm tra mã xác thực
            if (user.ResetPasswordToken != CodeXacThuc)
            {
                TempData["ErrorMessage"] = "Mã xác thực không đúng.";
                return View();
            }
            // Nếu mã xác thực đúng, chuyển hướng tới trang cập nhật mật khẩu
            HttpContext.Session.SetString("ResetPasswordUserName", user.UserName);
            return RedirectToAction("UpdatePassword");
        }
        public IActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdatePassword(string password)
        {
            var username = HttpContext.Session.GetString("ResetPasswordUserName");
            Console.WriteLine("Username from HttpContext: " + username); // Thêm log để ghi lại thông tin đăng nhập được lấy từ HttpContext
            var user = userServices.GetAllUser().FirstOrDefault(c => c.UserName == username);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User không tồn tại";
                return View();
            }
            user.Password = password;
            userServices.UpdateUser(user);
            TempData["UserName"] = user.UserName;
            TempData["Password"] = user.Password;
            TempData["SignUpSuccess"] = "Mật khẩu đã được cập nhật thành công!";
            return RedirectToAction("Login", "User");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
