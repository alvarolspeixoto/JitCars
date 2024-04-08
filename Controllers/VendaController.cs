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
            var vendas = await _db.Vendas.Include(e => e.Cliente)
                                         .Include(e => e.Funcionario)
                                         .ToListAsync();

            

            var carrosVendidos = _db.Carros.Where(e => e.VendaId != null).ToList();

            

            ViewBag.carros = carrosVendidos;
            

			return View(vendas);
		}

        //GET
        public async Task<IActionResult> Cadastrar()
        {
            var clientes = _db.Clientes.ToList();
            var funcionarios = await _db.Set<Funcionario>().ToListAsync();
            var carros = await _db.Carros.Include(e => e.Modelo).Include(e => e.Venda)
                                         .Where(e => e.VendaId == null || e.Venda.Status == Status.Cancelado).ToListAsync();



            ViewBag.clientes = clientes;
            ViewBag.funcionarios = funcionarios;
            ViewBag.pagamentos = Enum.GetValues(typeof(FormaPagamento));
            ViewBag.carros = carros;

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Venda obj, List<int> carrosSelecionados)
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

					
					if (carrosSelecionados != null && carrosSelecionados.Any())
					{
						obj.ClienteId = cliente.Id;
						_db.Vendas.Add(obj);
						await _db.SaveChangesAsync();
						foreach (var carroId in carrosSelecionados)
						{
                            var carro = await _db.Carros.FindAsync(carroId);
                            carro.VendaId = obj.Id;
                            _db.Update(carro);
						}
					}

                    await _db.SaveChangesAsync();
					return RedirectToAction("Index");
				}

			}
			ViewBag.Clientes = await _db.Clientes.ToListAsync();
			return View();
        }



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
            var funcionarios = _db.Set<Funcionario>().ToList();
			var carros =  _db.Carros.Include(e => e.Modelo).Include(e => e.Venda)
										 .Where(e => e.VendaId == null || e.Venda.Status == Status.Cancelado).ToList();

            ViewBag.carros = carros;
            ViewBag.clientes = clientes;
            ViewBag.funcionarios = funcionarios;
            ViewBag.pagamentos = Enum.GetValues(typeof(FormaPagamento));

			return View(vendaFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Venda obj, List<int> carrosSelecionados)
        {
            var cliente = await _db.Clientes.FindAsync(obj.ClienteId);
            var carroAntigo = _db.Carros.Where(c => c.VendaId == obj.Id).ToList();

            foreach(var c in carroAntigo)
            {
                c.VendaId = null;
            }

            if (cliente == null)
            {
                ModelState.AddModelError("ClienteId", "Selecione o cliente");
            }

            if (ModelState.IsValid)
            {
				if (carrosSelecionados != null && carrosSelecionados.Any())
				{
					
					foreach (var carroId in carrosSelecionados)
					{
						var carro = await _db.Carros.FindAsync(carroId);
                        
						carro.VendaId = obj.Id;
                        
						_db.Update(carro);
					}
				}

				obj.ClienteId = cliente.Id;
				_db.Vendas.Update(obj);
				await _db.SaveChangesAsync();
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
            var vendaFromDb = _db.Vendas.Include(e => e.Cliente)
                                        .Include(e => e.Funcionario)
                                        .FirstOrDefault(e => e.Id == id);

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
           var vendaCancelada = _db.Vendas.Find(id);
           if(vendaCancelada == null)
           {
                return NotFound();
           }

           vendaCancelada.Status = Status.Cancelado;
           _db.SaveChanges();
           return RedirectToAction("Index");         
        }




    }
}
