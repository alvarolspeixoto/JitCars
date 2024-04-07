using Microsoft.AspNetCore.Mvc;
using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using JitCars.Enums;

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
        public async Task<IActionResult> Index()
        {
            var clientes = await _db.Clientes.ToListAsync();
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            return View();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(ClienteEnderecoViewModel viewModel)
        {
            Cliente cliente = viewModel.Cliente;


            if (_db.Clientes.Any(c => c.Cpf == cliente.Cpf))
            {
                ModelState.AddModelError("Cliente.Cpf", "Este CPF já está em uso");
            }


            if (_db.Clientes.Any(c => c.Email == cliente.Email))
            {
                ModelState.AddModelError("Cliente.Email", "Este endereco de e-mail já está em uso");
            }


            if (_db.Clientes.Any(c => c.Telefone == cliente.Telefone))
            {
                ModelState.AddModelError("Cliente.Telefone", "Este telefone já está em uso");
            }


            if (ModelState.IsValid)
            {
                Endereco endereco = viewModel.Endereco;
                _db.Enderecos.Add(endereco);
                await _db.SaveChangesAsync();

                cliente.EnderecoId = endereco.Id;

                _db.Clientes.Add(cliente);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca o cliente pelo id fornecido, incluindo o endereço associado
            var cliente = await _db.Clientes
                                  .Include(c => c.Endereco)
                                  .FirstAsync(x => x.Id == id);

            // Se o cliente não for encontrado, retorna NotFound
            if (cliente == null)
            {
                return NotFound();
            }

            Endereco endereco = _db.Enderecos.First(x => x.Id == cliente.EnderecoId);



            if (endereco == null)
            {
                return NotFound();
            }
            // Cria um objeto ClienteEnderecoViewModel e configura com o cliente e os telefones
            ClienteEnderecoViewModel viewModel = new ClienteEnderecoViewModel
            {
                Cliente = cliente,
                Endereco = endereco
            };
            // Retorna a View com o cliente e seus dados relacionados
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ClienteEnderecoViewModel viewModel)
        {
            try
            {
                Cliente cliente = viewModel.Cliente;
                var clienteDoBanco = await _db.Clientes.Include(c => c.Endereco).FirstOrDefaultAsync(c => c.Id == cliente.Id);

                if (clienteDoBanco == null)
                {
                    return NotFound();
                }

                clienteDoBanco.PrimeiroNome = cliente.PrimeiroNome;
                clienteDoBanco.Sobrenome = cliente.Sobrenome;
                clienteDoBanco.Email = cliente.Email;
                clienteDoBanco.Telefone = cliente.Telefone;
                if (clienteDoBanco.Endereco == null)
                {
                    clienteDoBanco.Endereco = new Endereco(); 
                }
                clienteDoBanco.Endereco.Cep = cliente.Endereco.Cep;
                clienteDoBanco.Endereco.Rua = cliente.Endereco.Rua;
                clienteDoBanco.Endereco.Numero = cliente.Endereco.Numero;
                clienteDoBanco.Endereco.Bairro = cliente.Endereco.Bairro;
                clienteDoBanco.Endereco.Cidade = cliente.Endereco.Cidade;
                clienteDoBanco.Endereco.Estado = cliente.Endereco.Estado;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {

                ModelState.AddModelError("", "Os dados foram modificados por outro usuário. Por favor, tente novamente.");
                return View(viewModel);
            }




        }
    }
}





