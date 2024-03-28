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


		[HttpGet]
		public IActionResult Editar(int? id) 
		{
			if (id == null || id==0)
			{
				return NotFound	();
			}

			Cliente cliente = _db.Clientes.FirstOrDefault(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
		}


		[HttpGet]
		public IActionResult Deletar(int? id) 
		{
            if (id == null || id == 0)
            {
                return NotFound();
            }

			Cliente cliente = _db.Clientes.FirstOrDefault(x => x.Id == id);

			if (cliente == null)
			{
				return NotFound();
			}

			return RedirectToAction("Index");

        }




		[HttpPost]
		public IActionResult Cadastrar(ClienteEnderecoTelViewModel viewModel)
		{
			if(ModelState.IsValid) 
			{
                Cliente cliente = viewModel.Cliente;
                Endereco endereco = viewModel.Endereco;
				Telefone telefone = viewModel.Telefone;

                _db.Clientes.Add(cliente);
                _db.Enderecos.Add(endereco);
                _db.Telefones.Add(telefone);
                _db.SaveChanges();

				return RedirectToAction("Index");
			}

			return View();	
		}

		[HttpPost]
		public IActionResult Editar(ClienteEnderecoTelViewModel viewModel) 
		{

			if (ModelState.IsValid)
			{
                Cliente cliente = viewModel.Cliente;
                Endereco endereco = viewModel.Endereco;
                Telefone telefone = viewModel.Telefone;

                _db.Clientes.Update(cliente);
				_db.Enderecos.Update(endereco);
				_db.Telefones.Update(telefone);
				_db.SaveChanges();

				return RedirectToAction("Index");
			}

			return View(viewModel);
		}


		[HttpPost]
		public IActionResult Deletar(ClienteEnderecoTelViewModel viewModel)
		{
			Cliente cliente = viewModel.Cliente;
			Endereco endereco = viewModel.Endereco;
			Telefone telefone = viewModel.Telefone;

            if (cliente== null)
			{
				return NotFound();
			}

			_db.Clientes.Remove(cliente);
			_db.SaveChanges();

			return RedirectToAction("Index");
        }
    }
}
