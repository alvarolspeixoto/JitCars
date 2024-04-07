using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<Funcionario> _signInManager;
        private readonly UserManager<Funcionario> _userManager;

        public FuncionarioController(
            AppDbContext context,
            SignInManager<Funcionario> signInManager,
            UserManager<Funcionario> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _context.Set<Funcionario>()
                .Include(f => f.Endereco)
                .Include(f => f.Cargo)
                .ToListAsync();

            return View(funcionarios);
        }
        [HttpGet]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Registrar()
        {

            ViewBag.cargos = await _context.Cargos.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {

            var usuarioCpfExistente = await _context.Users
                                            .OfType<Funcionario>()
                                            .SingleOrDefaultAsync(e => e.Cpf == model.Cpf);

            if (usuarioCpfExistente != null)
            {
                ModelState.AddModelError("Cpf", "Este CPF já está em uso");
            }

            var usuarioUserExistente = await _context.Users
                                            .OfType<Funcionario>()
                                            .SingleOrDefaultAsync(e => e.UserName == model.NomeUsuario);

            if (usuarioUserExistente != null)
            {
                ModelState.AddModelError("NomeUsuario", "Este nome de usuário já está em uso");
            }

            if (ModelState.IsValid)
            {

                Endereco endereco = model.Endereco!;
                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();

                Funcionario funcionario = new()
                {
                    PrimeiroNome = model.PrimeiroNome,
                    Sobrenome = model.Sobrenome,
                    UserName = model.NomeUsuario,
                    Cpf = model.Cpf,
                    EnderecoId = endereco.Id,
                    CargoId = model.CargoId
                };

                var senhaPadrao = "Senha@" + funcionario.Cpf;
                var resultado = await _userManager.CreateAsync(funcionario, senhaPadrao);
                var cargo = await _context.Cargos.FindAsync(model.CargoId);
            
                if (cargo!.Titulo == "Gerente") await _userManager.AddToRoleAsync(funcionario, "Gerente");

                if (resultado.Succeeded)
                {

                    return RedirectToAction("Index", "Funcionario");
                }

                foreach (var erro in resultado.Errors)
                {
                    ModelState.AddModelError("", erro.Description);
                }
            }

            ViewBag.cargos = await _context.Cargos.ToListAsync();

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.NomeUsuario!, model.Senha!, model.ManterLogado, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("LoginFailure", "Tentativa de login inválida");

                return View(model);
            }

            return View();

        }

        [AllowAnonymous]
        public async Task<IActionResult> Deslogar()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //GET
        public IActionResult Cadastrar()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gerente")]
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


            var funcionario = await _context.Users
               .OfType<Funcionario>()
               .Include(f => f.Endereco)
               .Include(f => f.Cargo)
               .FirstOrDefaultAsync(f => f.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewBag.Cargos = await _context.Cargos.ToListAsync();

            return View(funcionario);
        }

        [HttpPost]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Atualizar(Funcionario obj)
        {
            try
            {
                var funcionarioDoBanco = await _context.Users.FindAsync(obj.Id);


                funcionarioDoBanco.PrimeiroNome = obj.PrimeiroNome;
                funcionarioDoBanco.Sobrenome = obj.Sobrenome;
                if (funcionarioDoBanco.Endereco == null)
                {
                    funcionarioDoBanco.Endereco = new Endereco();
                }
                funcionarioDoBanco.Endereco.Cep = obj.Endereco.Cep;
                funcionarioDoBanco.Endereco.Rua = obj.Endereco.Rua;
                funcionarioDoBanco.Endereco.Numero = obj.Endereco.Numero;
                funcionarioDoBanco.Endereco.Bairro = obj.Endereco.Bairro;
                funcionarioDoBanco.Endereco.Cidade = obj.Endereco.Cidade;
                funcionarioDoBanco.Endereco.Estado = obj.Endereco.Estado;
                funcionarioDoBanco.CargoId = obj.CargoId;





                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
               
                ModelState.AddModelError("", "Os dados foram modificados por outro usuário. Por favor, tente novamente.");
                return View(obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Perfil()
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return NotFound();
            }

            var id = (await _userManager.GetUserAsync(User))!.Id;

            var usuario = await _context.Users
                                .Include(u => u.Cargo)
                                .Include(u => u.Endereco)
                                .FirstOrDefaultAsync(u => u.Id == id);

            ViewBag.usuario = usuario;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MudarSenha(MudarSenhaViewModel model)
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return NotFound();
            }

            var id = (await _userManager.GetUserAsync(User))!.Id;

            var usuario = await _context.Users
                                .Include(u => u.Cargo)
                                .Include(u => u.Endereco)
                                .FirstOrDefaultAsync(u => u.Id == id);

            ViewBag.usuario = usuario;

            var resultado = await _userManager.CheckPasswordAsync(usuario!, model.SenhaAtual!);

            if (!resultado)
            {
                ModelState.AddModelError("SenhaAtual", "A senha está incorreta");
            }

            if (!ModelState.IsValid) return View("Perfil", model);

            var resultadoSenha = await _userManager.ChangePasswordAsync(usuario!, model.SenhaAtual!, model.NovaSenha!);

            if (!resultadoSenha.Succeeded)
            {
                foreach (var erro in resultadoSenha.Errors) {

                    ModelState.AddModelError("", erro.Description);
                }
                return View("Perfil", model);
            }

            TempData["sucesso"] = "Senha alterada com sucesso!";

            return RedirectToAction("Perfil", "Funcionario");

        }





    }
}
