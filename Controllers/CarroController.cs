using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers
{

    public class CarroController : Controller
    {

        private readonly AppDbContext _context;

        public CarroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carros = await _context.Carros.Include(e => e.Modelo).ToListAsync();

            return View(carros);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var modelos = _context.Modelos.ToList();

            ViewBag.modelos = modelos;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Carro carro)
        {

            var modelo = await _context.Modelos.FindAsync(carro.ModeloId);

            if (modelo == null)
            {
                ModelState.AddModelError("ModeloId", "Selecione o modelo do carro");
            }

            if (ModelState.IsValid)
            {
                _context.Add(carro);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index", "Carro");
            }

            ViewBag.modelos = await _context.Modelos.ToListAsync();
            return View(carro);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
            .Include(e => e.Modelo)
            .FirstOrDefaultAsync(e => e.Id == id);

            if (carro == null)
            {
                return NotFound();
            }

            var modelos = _context.Modelos.ToList();
            ViewBag.modelos = modelos;

            return View(carro);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Carro carro)
        {
            var modelo = await _context.Modelos.FindAsync(carro.ModeloId);

            if (modelo == null)
            {
                ModelState.AddModelError("ModeloId", "Selecione o modelo do carro");
            }

            if (ModelState.IsValid)
            {
                _context.Carros.Update(carro);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(carro);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
                .Include(e => e.Modelo)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros.FindAsync(id);

            if (carro == null)
            {
                return NotFound();
            }

            _context.Remove(carro);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }


}