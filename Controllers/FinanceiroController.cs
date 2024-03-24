using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers
{
    public class FinanceiroController : Controller
    {

        private readonly AppDbContext dbase;

        public FinanceiroController(AppDbContext db)
        {
            dbase = db;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Carros = await dbase.Carros.Include(e => e.Modelo).ToListAsync();
            ViewBag.Vendas = dbase.Vendas.ToList();

            return View();
        }

        public IActionResult ObterNome()
        {
           
            return View();
        }
    }
}
