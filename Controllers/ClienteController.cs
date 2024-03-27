using Microsoft.AspNetCore.Mvc;
using JitCars.Data;
using JitCars.Models;

namespace JitCars.Controllers
{
	public class ClienteController : Controller
	{

		readonly private AppDbContext _db;
		public ClienteController(AppDbContext db) 
		{
			_db = db;
		}
		public IActionResult Index()
		{
			IEnumerable<Cliente> clientes = _db.Clientes;
			return View(clientes);
		}

		public IActionResult Cadastrar()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Cadastrar(Cliente cliente)
		{
			if(ModelState.IsValid) 
			{
				_db.Clientes.Add(cliente);
                _db.SaveChanges();

				return RedirectToAction("Index");
			}

			return View();	
		}

	}
}
