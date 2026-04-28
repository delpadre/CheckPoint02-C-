using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using GameStoreMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreMVC.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        private readonly ILogger<HomeController> _logger;
        private readonly IGameRepository _gameRepositorio;

        public HomeController(ILogger<HomeController> logger, IGameRepository gameRepositorio)
=======
        private readonly IGameRepository _gameRepository;

        public HomeController(IGameRepository gameRepository)
>>>>>>> addd0cac9249feb46ffc9a9a2b23010abc489077
        {
            _gameRepository = gameRepository;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _gameRepository.GetAllAsync();
            return View(games);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
//teste