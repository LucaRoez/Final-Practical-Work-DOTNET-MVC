using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrabajoFinal___Bingo_Web_MVC_Service.Models;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() => View();

        public IActionResult StartBingo() => View();

        public IActionResult AboutMe() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}