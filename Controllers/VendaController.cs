﻿using JitCars.Data;
using JitCars.Models;
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
            var vendas = await _db.Vendas.Include(e => e.Cliente).ToListAsync();
			return View(vendas);
		}

        //GET
        public IActionResult Registrar()
        {
            
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(Venda obj)
        {
            if(ModelState.IsValid)
            {
                _db.Vendas.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
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

            return View(vendaFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Venda obj)
        {
            if (ModelState.IsValid)
            {
                _db.Vendas.Update(obj);
                _db.SaveChanges();
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
            var vendaFromDb = _db.Vendas.Find(id);

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
           var objToDelete = _db.Vendas.Find(id);
           if(objToDelete == null)
           {
                return NotFound();
           }

           _db.Vendas.Remove(objToDelete);
           _db.SaveChanges();
           return RedirectToAction("Index");         
        }




    }
}
