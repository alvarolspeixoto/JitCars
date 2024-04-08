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
                DateTime horaAtualUtc = DateTime.UtcNow;
                DateTime horaAtualBrasil = horaAtualUtc.AddHours(-3);
                obj.Date = horaAtualBrasil;
                _db.Cargos.Add(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public async Task<IActionResult> Editar(int? id)
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
        public async Task<IActionResult> Editar(Cargo obj)
        {
            if (ModelState.IsValid)
            {
                
                var cargoOriginal = await _db.Cargos.FindAsync(obj.Id);
                if (cargoOriginal == null)
                {
                    return NotFound(); 
                }

               
                var horaAtualBrasil = DateTime.UtcNow.AddHours(-3);
                obj.Date = horaAtualBrasil;

                _db.Entry(cargoOriginal).CurrentValues.SetValues(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }




    }
}
