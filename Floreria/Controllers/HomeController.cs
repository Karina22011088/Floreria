using Floreria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Floreria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Flores()
        {
            return View();
        }

        public IActionResult Arreglos()
        {
            return View();
        }

        public IActionResult Entrega()
        {
            return View();
        }

        public IActionResult Boletin()
        {
            return View();
        }

        public IActionResult Soporte()
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
