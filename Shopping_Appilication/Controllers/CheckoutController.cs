using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping_Appilication.Models;
using System.Diagnostics;

namespace Shopping_Appilication.Controllers
{
    public class CheckoutController:Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly ShopDBContext _shopDBContext;
        public CheckoutController(ILogger<RoleController> logger)
        {
            _logger = logger;
            _shopDBContext = new ShopDBContext();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
