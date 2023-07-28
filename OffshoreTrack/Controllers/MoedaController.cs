using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace OffshoreTrack.Controllers
{
    [Authorize]
    public class MoedaController : Controller
    {

        private readonly Contexto contexto;

        public MoedaController (Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var moeda = await contexto.Moeda.Where(c => c.Deletado != true).ToListAsync();
            return View(moeda);
        }

        /* CRUD */

        // Create
        [HttpGet]
        public IActionResult New()
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("moeda_descricao, simbolo")] Moeda createRequest)
        {
            var moeda = new Moeda
            {
                moeda_descricao = createRequest.moeda_descricao,
                simbolo = createRequest.simbolo
            };

            contexto.Moeda.Add(moeda);
            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Fim - Create

        // Read
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var moeda = await contexto.Moeda.FirstOrDefaultAsync(x => x.id_moeda == id);
            return View(moeda);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var moeda = await contexto.Moeda.FirstOrDefaultAsync(x => x.id_moeda == id);
            if (moeda == null)
            {
                return NotFound();
            }
            return View(moeda);
        }

       [HttpPost]
        public async Task<IActionResult> Update(Moeda updateRequest)
        {
            var moeda = await contexto.Moeda.FindAsync(updateRequest.id_moeda);
            if (moeda == null)
            {
                return NotFound();
            }
            moeda.moeda_descricao = updateRequest.moeda_descricao;
            moeda.simbolo = updateRequest.simbolo;
            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Fim - Update

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Moeda deleteRequest)
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var moeda = await contexto.Moeda.FindAsync(deleteRequest.id_moeda);

            if (moeda == null)
            {
                return NotFound();
            }
            moeda.Deletado = true;
            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Fim - Delete
        /* Fim - CRUD */
    }
}