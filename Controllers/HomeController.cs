using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;
using JitCars.Data;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _db;

	public HomeController(AppDbContext db)
	{
		_db = db;
	}

	public IActionResult Index()
    {
        var carros = _db.Carros.Include(c => c.Venda)
                               .Include(c => c.Modelo)
							   .Where(c => c.VendaId != null).ToList();

		var modelosMaisVendidos = _db.Carros.Where(c => c.VendaId != null)
											.GroupBy(c => c.Modelo) 
											.Select(g => new
											{
											Modelo = g.Key.Marca + " " + g.Key.Nome, 
											QuantidadeVendas = g.Count() 
											})
											.OrderByDescending(m => m.QuantidadeVendas) 
											.Take(5) 
											.ToList();


		
		var topVendedores = _db.Vendas.Include(v => v.Funcionario).GroupBy(v => v.Funcionario)
									  .Select(g => new
									  {
                                          Nome = g.Key.PrimeiroNome + " " + g.Key.Sobrenome, // Obtém o nome completo do funcionário
                                          QuantidadeVendas = g.Count()
                                      })
                                      .OrderByDescending(f => f.QuantidadeVendas) 
                                      .Take(5) 
                                      .ToList();

        

        ViewBag.carros = carros;
		ViewBag.modelos = modelosMaisVendidos;
        ViewBag.funcionarios = topVendedores;


        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
