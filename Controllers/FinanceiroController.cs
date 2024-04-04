using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Controllers
{
    [Authorize]
    public class FinanceiroController : Controller
    {

        private readonly AppDbContext dbase;

        public FinanceiroController(AppDbContext db)
        {
            dbase = db;
        }
        public async Task<IActionResult> Index(string dataSelecionada)
        {
            var data = new SelectList(new List<String> { "03/2024", "02/2024", "01/2024" }); ;
            ViewData["data"] = data;
            if (!string.IsNullOrEmpty(dataSelecionada))
            {
                // Converte a string da data selecionada para um objeto DateTime
                if (!DateTime.TryParse(dataSelecionada, out DateTime dataSelecionadaDateTime))
                {
                    // Trate o caso em que a dataSelecionada não pode ser convertida para DateTime
                    return Content("Data selecionada inválida");
                }

                // Extrai o mês e o ano da data selecionada
                int mesSelecionado = dataSelecionadaDateTime.Month;
                int anoSelecionado = dataSelecionadaDateTime.Year;

                // Filtra os carros de acordo com o mês e ano selecionados
                var carrosFiltrados = await dbase.Carros
                    .Where(c => c.Venda != null && c.Venda.Data.Month == mesSelecionado && c.Venda.Data.Year == anoSelecionado)
                    .Include(e => e.Modelo)
                    .ToListAsync();

                ViewBag.Carros = carrosFiltrados;
            }
            else
            {
                // Trata o caso em que dataSelecionada é nula ou vazia
                ViewBag.Carros = await dbase.Carros.Include(e => e.Modelo).ToListAsync();
            }

            
            ViewBag.Funcionarios = await dbase.Set<Funcionario>()
                .Include(f => f.Endereco)
                .Include(f => f.Cargo)
                .ToListAsync();
            ViewBag.Vendas = await dbase.Vendas.Include(c => c.Cliente).Include(f => f.Funcionario).ToListAsync();
            return View();


        }



        
    }
}
