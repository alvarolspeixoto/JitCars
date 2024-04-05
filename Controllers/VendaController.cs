using JitCars.Data;
using JitCars.Models;
using JitCars.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers
{
    [Authorize]
	public class VendaController : Controller
	{
		private readonly AppDbContext _db;

        public VendaController(AppDbContext db)
        {
			_db = db;
        }

        public async Task<IActionResult> Index()
		{
            var vendas = await _db.Vendas.Include(e => e.Cliente).ToListAsync();
			return View(vendas);
		}

        //GET
        public IActionResult Registrar()
        {
            var clientes = _db.Clientes.ToList();

            ViewBag.clientes = clientes;
            ViewBag.pagamentos = Enum.GetValues(typeof(FormaPagamento));
            
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(Venda obj)
        {

			var cliente = await _db.Clientes.FindAsync(obj.ClienteId);

			if (ModelState.IsValid)
			{
				if (cliente == null)
				{
					ModelState.AddModelError("ClienteId", "Selecione um CPF válido");
				}
				else
				{
					obj.ClienteId = cliente.Id;
					_db.Vendas.Add(obj);
					await _db.SaveChangesAsync();
					return RedirectToAction("Index");
				}

			}
			ViewBag.Clientes = await _db.Clientes.ToListAsync();
			return View();
        }

		/*public JsonResult Autocomplete(string prefix)
		{
			List<String> clientes = _db.Clientes.Where(c => c.Cpf.StartsWith(prefix)).Select(x => x.Cpf).ToList();
			return Json(clientes);

		}
        */

		//GET
		public IActionResult Editar(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var vendaFromDb = _db.Vendas.Find(id);

            if(vendaFromDb == null)
            {
                return NotFound();
            }

            var clientes = _db.Clientes.ToList();
            ViewBag.Clientes = clientes;

            return View(vendaFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Venda obj)
        {
            if (ModelState.IsValid)
            {
                _db.Vendas.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Deletar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var vendaFromDb = _db.Vendas.Find(id);

            if (vendaFromDb == null)
            {
                return NotFound();
            }

            return View(vendaFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletarPOST(int? id)
        {
           var objToDelete = _db.Vendas.Find(id);
           if(objToDelete == null)
           {
                return NotFound();
           }

           _db.Vendas.Remove(objToDelete);
           _db.SaveChanges();
           return RedirectToAction("Index");         
        }




    }
}
