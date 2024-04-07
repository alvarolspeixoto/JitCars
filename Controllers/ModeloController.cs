using JitCars.Data;
using JitCars.Enums;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers
{
    [Authorize]
    public class ModeloController(AppDbContext context) : Controller
    {

        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var modelos = await _context.Modelos.ToListAsync();

            return View(modelos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.carrocerias = Enum.GetValues(typeof(Carroceria));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                _context.Modelos.Add(modelo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Modelo");
            }

            ViewBag.carrocerias = Enum.GetValues(typeof(Carroceria));
            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var modelo = await _context.Modelos.FindAsync(id);

            if (modelo == null) return NotFound();

            ViewBag.carrocerias = Enum.GetValues(typeof(Carroceria));
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Modelo modelo)
        {

            if (ModelState.IsValid)
            {
                _context.Modelos.Update(modelo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(modelo);

        }

        [HttpGet]
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var modelo = await _context.Modelos.FindAsync(id);

            if (modelo == null) return NotFound();

            return View(modelo);
        }

        [HttpPost, ActionName("Deletar")]
        public async Task<IActionResult> DeletarPOST(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var modelo = await _context.Modelos.FindAsync(id);

            if (modelo == null) return NotFound();

            _context.Modelos.Remove(modelo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Modelo");

        }

    }
}
