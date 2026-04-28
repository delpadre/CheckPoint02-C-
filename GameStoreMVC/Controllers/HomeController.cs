using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using GameStoreMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public HomeController(IGameRepository gameRepository)
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