

using gamerzone_web_mvc_mongo.Models;
using gamerzone_web_mvc_mongo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace gamerzone_web_mvc_mongo.Controllers
{
    public class VideojuegosController : Controller
    {
        private readonly VideojuegoService _videojuegoService;

        public VideojuegosController(VideojuegoService videojuegoService)
        {
            _videojuegoService = videojuegoService;
        }

        public async Task<IActionResult> Index()
        {
            var videojuegos = await _videojuegoService.GetAllAsync();
            ViewBag.Total = await _videojuegoService.CountAsync();
            return View(videojuegos);
        }

        public async Task<IActionResult> Details(string id)
        {
            var videojuego = await _videojuegoService.GetByIdAsync(id);
            if (videojuego == null) return NotFound();
            return View(videojuego);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Generos = await _videojuegoService.GetGenerosAsync();
            ViewBag.Paises = await _videojuegoService.GetPaisesAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Videojuego videojuego, string plataformasTexto)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(plataformasTexto))
                {
                    videojuego.Plataformas = plataformasTexto
                                                .Split(',')
                                                .Select(p => p.Trim())
                                                .Where(p => !string.IsNullOrEmpty(p))
                                                .ToList();
                }
                await _videojuegoService.CreateAsync(videojuego);
                TempData["Mensaje"] = "Videojuego creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(videojuego);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var videojuego = await _videojuegoService.GetByIdAsync(id);
            if (videojuego == null) return NotFound();

            ViewBag.PlataformasText = string.Join(", ", videojuego.Plataformas);
            ViewBag.Generos = await _videojuegoService.GetGenerosAsync();
            ViewBag.Paises = await _videojuegoService.GetPaisesAsync();
            return View(videojuego);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Videojuego videojuego, string plataformasTexto)
        {
            if (id != videojuego.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(plataformasTexto))
                {
                    videojuego.Plataformas = plataformasTexto
                                                .Split(',')
                                                .Select(p => p.Trim())
                                                .Where(p => !string.IsNullOrEmpty(p))
                                                .ToList();
                }
                await _videojuegoService.UpdateAsync(id, videojuego);
                TempData["Mensaje"] = "Videojuego actualizado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(videojuego);
        }


        public async Task<IActionResult> Delete(string id)
        {
            var videojuego = await _videojuegoService.GetByIdAsync(id);
            if (videojuego == null) NotFound();

            return View(videojuego);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _videojuegoService.DeleteAsync(id);
            TempData["Mensaje"] = "Videojuego eliminado exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }

}