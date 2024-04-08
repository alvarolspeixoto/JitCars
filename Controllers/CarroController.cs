using JitCars.Data;
using JitCars.Enums;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers
{
    [Authorize]
    public class CarroController : Controller
    {

        private readonly AppDbContext _context;

        public CarroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Cor? cor, Carroceria? carroceria, int? ano)
        {
            var carrosQuery = _context.Carros.Include(e => e.Modelo)
                                              .Include(e => e.Venda)
                                              .Where(e => e.Venda == null || e.Venda.Status == Status.Cancelado);

            if (cor.HasValue)
            {
                carrosQuery = carrosQuery.Where(e => e.Cor == cor.Value);
            }

            if (carroceria.HasValue)
            {
                carrosQuery = carrosQuery.Where(e => e.Modelo.Carroceria == carroceria.Value);
            }

            if (ano.HasValue)
            {
                carrosQuery = carrosQuery.Where(e => e.Modelo != null && e.Modelo.AnoFabricacao == ano.Value);
            }


            ViewBag.cores = Enum.GetValues(typeof(Cor));
            ViewBag.carroceria = Enum.GetValues(typeof(Carroceria));
            ViewBag.anos = _context.Modelos.Select(m => m.AnoFabricacao).Distinct().ToList();

            var carros = await carrosQuery.ToListAsync();

            return View(carros);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            var modelos = _context.Modelos.ToList();

            ViewBag.modelos = modelos;
            ViewBag.cores = Enum.GetValues(typeof(Cor));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Carro carro)
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

        public async Task<IActionResult> Editar(int? id)
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
            ViewBag.cores = Enum.GetValues(typeof(Cor));

            return View(carro);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Carro carro)
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
        public async Task<IActionResult> Deletar(int? id)
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

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarPOST(int? id)
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