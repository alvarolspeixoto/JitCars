using Microsoft.AspNetCore.Mvc;
using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
        /* public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca o cliente pelo id fornecido, incluindo o endereço e o telefone associados
            Cliente cliente = _db.Clientes
                                  .Include(c => c.Endereco)
                                  .First(x => x.Id == id);

            // Se o cliente não for encontrado, retorna NotFound
            if (cliente == null)
            {
                return NotFound();
            }

            Endereco endereco = _db.Enderecos.First(x => x.Id == cliente.EnderecoId);


            // Busca os telefones associados ao cliente pelo ClienteId
            Telefone telefone = _db.Telefones
                                             .Where(t => t.ClienteId == cliente.Id)
                                             .First();

            if (endereco == null)
            {
                return NotFound();
            }

            // Cria um objeto ClienteEnderecoTelViewModel e configura com o cliente e os telefones
            ClienteEnderecoTelViewModel viewModel = new ClienteEnderecoTelViewModel
            {
                Cliente = cliente,
                Telefone = telefone,
                Endereco = endereco
            };
            // Retorna a View com o cliente e seus dados relacionados
            return View(viewModel);
        } */

        /*
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
		*/


        [HttpPost]
        public IActionResult Cadastrar(ClienteEnderecoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = viewModel.Cliente;
                Endereco endereco = viewModel.Endereco;
                // Telefone telefone = viewModel.Telefone;

                _db.Enderecos.Add(endereco);
                _db.SaveChanges();

                cliente.EnderecoId = endereco.Id;
                _db.Clientes.Add(cliente);
                _db.SaveChanges();

                // telefone.ClienteId = cliente.Id;
                // _db.Telefones.Add(telefone);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ClienteEnderecoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = viewModel.Cliente;
                Endereco endereco = viewModel.Endereco;
                // Telefone telefone = viewModel.Telefone;

                // Verifica se o telefone foi modificado
                /* Telefone telefoneNoBanco = _db.Telefones.First(t => t.Id == telefone.Id);
                if (telefoneNoBanco != null &&
                    (telefoneNoBanco.Numero != telefone.Numero || telefoneNoBanco.ClienteId != telefone.ClienteId))
                {
                    // Se o telefone foi modificado, atualiza-o
                    _db.Telefones.Update(telefone);
                } */

                // Verifica se o ID do endereço é válido
                if (cliente.Endereco != null && cliente.Endereco.Id > 0)
                {
                    // Verifica se o endereço associado ao cliente está carregado corretamente
                    endereco = _db.Enderecos.FirstOrDefault(e => e.Id == cliente.Endereco.Id);
                    if (endereco != null)
                    {
                        // Mantém o "EnderecoId" original, a menos que o endereço tenha sido alterado
                        if (cliente.EnderecoId != endereco.Id)
                        {
                            cliente.EnderecoId = endereco.Id;
                        }
                        // Atualiza o endereço associado ao cliente
                        _db.Enderecos.Update(endereco);
                    }



                    // Atualiza o cliente
                    _db.Clientes.Update(cliente);

                    // Atualiza o telefone associado ao cliente (se existir)
                    // _db.Telefones.Update(telefone);


                    // Salva as alterações no banco de dados
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }

                // Se o modelo não for válido, retorna a View com o modelo para exibir os erros de validação
            }
            return View(viewModel);

            /*
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
            */


        }
    }
}
