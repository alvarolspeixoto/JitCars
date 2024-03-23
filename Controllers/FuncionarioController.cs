using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly AppDbContext _context;

        public FuncionarioController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _context.Set<Funcionario>()
                .Include(f => f.Endereco) 
                .Include(f => f.Cargo) 
                .ToListAsync();

            return View(funcionarios);
        }

        //GET
        public IActionResult Cadastrar()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(Funcionario obj)
        {
            if (ModelState.IsValid)
            {
                _context.Set<Funcionario>().Add(obj); 
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public async Task<IActionResult> Atualizar(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Set<Funcionario>().FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Atualizar(Funcionario obj)
        {
            if (ModelState.IsValid)
            {
                _context.Set<Funcionario>().Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }




    }
}
