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


    }


}