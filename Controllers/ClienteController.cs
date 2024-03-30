using Microsoft.AspNetCore.Mvc;
using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;

namespace JitCars.Controllers
{
	[Authorize]
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

            if (id==null)
			{
				return NotFound	();
			}

            
            Cliente cliente = _db.Clientes.First(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            Endereco endereco = _db.Enderecos.First(x => x.Id == cliente.EnderecoId);

            if (endereco == null)
            {
                return NotFound();
            }

            cliente.EnderecoId = endereco.Id;

            Telefone telefone = _db.Telefones.First(x => x.ClienteId == cliente.Id);

			if (telefone == null)
			{
				return NotFound();
			}

            telefone.ClienteId = cliente.Id; 

            ClienteEnderecoTelViewModel viewModel = new ClienteEnderecoTelViewModel
            {
                Cliente = cliente,
                Telefone = telefone,
                Endereco = endereco
            };

            return View(viewModel);
        }



		[HttpPost]
		public IActionResult Cadastrar(ClienteEnderecoTelViewModel viewModel)
		{
			if(ModelState.IsValid) 
			{
                Cliente cliente = viewModel.Cliente;
                Endereco endereco = viewModel.Endereco;
				Telefone telefone = viewModel.Telefone;

                _db.Enderecos.Add(endereco);
				_db.SaveChanges();

				cliente.EnderecoId = endereco.Id;
				_db.Clientes.Add(cliente);
				_db.SaveChanges();

				telefone.ClienteId = cliente.Id;
				_db.Telefones.Add(telefone);
				_db.SaveChanges();

				return RedirectToAction("Index");
			}

			return View();	
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ClienteEnderecoTelViewModel viewModel) 
		{

			if (ModelState.IsValid)
			{
                Cliente cliente = viewModel.Cliente;
                Endereco endereco = viewModel.Endereco;
                Telefone telefone = viewModel.Telefone;
                
                

                _db.Enderecos.Update(endereco);
                _db.SaveChanges();

                cliente.EnderecoId = endereco.Id;
                _db.Clientes.Update(cliente);
                _db.SaveChanges();

                telefone.ClienteId = cliente.Id;
                _db.Telefones.Update(telefone);
                _db.SaveChanges();
      

				return RedirectToAction("Index");
			}

			return View();
		}

    }
}
