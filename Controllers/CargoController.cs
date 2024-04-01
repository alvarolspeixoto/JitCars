using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JitCars.Data;
using JitCars.Models;

namespace JitCars.Controllers
{
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
        public async Task<IActionResult> Cadastrar([Bind("Id,Titulo,Date,Salario")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                NormalizeDateTime(cargo); //Para não dar erro precisa normalizar a data e hora antes de salvar
                _db.Add(cargo);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET
        public async Task<IActionResult> Atualizar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _db.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(int id, [Bind("Id,Titulo,Date,Salario")] Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    NormalizeDateTime(cargo); //Para não dar erro precisa normalizar a data e hora antes de salvar
                    _db.Update(cargo);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExiste(cargo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        private bool CargoExiste(int id)
        {
            return _db.Cargos.Any(e => e.Id == id);
        }

        private void NormalizeDateTime(Cargo cargo)
        {
            // Verificar se a data e hora não estão no formato UTC , se não  convertê-las para UTC
            if (cargo.Date.Kind != DateTimeKind.Utc)
            {
                cargo.Date = cargo.Date.ToUniversalTime();
            }
        }
    }
}
