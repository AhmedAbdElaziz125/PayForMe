using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayForMe1._1.Models;
using System.Diagnostics;

namespace PayForMe1._1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PayForMe_DBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, PayForMe_DBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var orders = _dbContext.Orders
                .Include(o => o.MainService)
                .Include(o => o.MiddleService)
                .Include(o => o.LastService)
                .Include(o => o.User).ToList();

            return View(orders);
        }

        public IActionResult Privacy()
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