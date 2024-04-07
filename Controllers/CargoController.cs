using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JitCars.Controllers
{
    [Authorize]
    public class CargoController : Controller
    {
        private readonly AppDbContext _db;

        public CargoController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var cargos = await _db.Cargos.ToListAsync();
            return View(cargos);
        }

        // GET
        public IActionResult Cadastrar()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Cargo obj)
        {
            if (ModelState.IsValid)
            {
                obj.Date = DateTime.Now;
                _db.Cargos.Add(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public async Task<IActionResult> Atualizar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var cargoFromDb = await _db.Cargos.FindAsync(id);
            if (cargoFromDb == null)
            {
                return NotFound();
            }
            return View(cargoFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(Cargo obj)
        {
            if (ModelState.IsValid)
            {
                obj.Date = DateTime.Now;
                _db.Cargos.Update(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        

        
    }
}
