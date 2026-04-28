using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using GameStoreMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreMVC.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _gameRepository.GetAllAsync();
            return View(games);
        }

        public async Task<IActionResult> Details(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null) return NotFound();
            return View(game);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Game game)
        {
            if (!ModelState.IsValid)
                return View(game);

            game.CriadoEm = DateTime.UtcNow;
            await _gameRepository.AddAsync(game);
            TempData["Sucesso"] = "Jogo cadastrado com sucesso!";
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null) return NotFound();
            return View(game);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Game game)
        {
            if (!ModelState.IsValid)
                return View(game);

            await _gameRepository.UpdateAsync(game);
            TempData["Sucesso"] = "Jogo atualizado com sucesso!";
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            await _gameRepository.DeleteAsync(id);
            TempData["Sucesso"] = "Jogo excluído com sucesso!";
            return RedirectToAction("Index", "Home");
        }
    }
}
//atualizando