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
    public class TipoController : Controller
    {

        private readonly Contexto contexto;

        public TipoController (Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tipo = await contexto.Tipo.ToListAsync();
            return View(tipo);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("tipo")] Tipo createRequest)
        {
            var tipo = new Tipo
            {
                tipo = createRequest.tipo
            };

            contexto.Tipo.Add(tipo);
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
            var tipo = await contexto.Tipo.FirstOrDefaultAsync(x => x.id_tipo == id);
            return View(tipo);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tipo = await contexto.Tipo.FirstOrDefaultAsync(x => x.id_tipo == id);
            if (tipo == null)
            {
                return NotFound();
            }
            return View(tipo);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Tipo updateRequest)
        {
            var tipo = await contexto.Tipo.FindAsync(updateRequest.id_tipo);
            if (tipo == null)
            {
                return NotFound();
            }

            tipo.tipo = updateRequest.tipo;
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
        public async Task<IActionResult> Delete(Tipo deleteRequest)
        {
            var tipo = await contexto.Tipo.FindAsync(deleteRequest.id_tipo);

            if (tipo == null)
            {
                return NotFound();
            }
            contexto.Tipo.Remove(tipo);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */

    }
}