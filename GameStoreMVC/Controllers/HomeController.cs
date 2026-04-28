using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameRepository _gameRepositorio;

        public HomeController(ILogger<HomeController> logger, IGameRepository gameRepositorio)
        {
            _logger = logger;
            _gameRepositorio = gameRepositorio;
        }

        public IActionResult Index()
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
