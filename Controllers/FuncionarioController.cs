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
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _context.Set<Funcionario>()
                .Include(f => f.Endereco)
                .Include(f => f.Cargo)
                .ToListAsync();

            return View(funcionarios);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar()
        {

            ViewBag.cargos = await _context.Cargos.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

                var resultado = await _userManager.CreateAsync(funcionario, model.Senha!);

                if (resultado.Succeeded)
                {
                    await _signInManager.SignInAsync(funcionario, isPersistent: false);

                    return RedirectToAction("Index", "Home");
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
