using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;


namespace OffshoreTrack.Controllers
{
    public class LocalController : Controller
    {
        private readonly Contexto contexto;

        public LocalController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var locals = await contexto.Local.ToListAsync();
            return View(locals);
        }
        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("local,id_cliente")] Local createRequest)
        {
            var locais = new Local
            {
                local = createRequest.local,
                id_cliente = createRequest.id_cliente
            };

            contexto.Local.Add(locais);
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
            var local = await contexto.Local.FirstOrDefaultAsync(x => x.id_local == id);
            return View(local);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var local = await contexto.Local.FirstOrDefaultAsync(x => x.id_local == id);
            if (local == null)
            {
                return NotFound();
            }
            return View(local);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Local updateRequest)
        {
            var local = await contexto.Local.FindAsync(updateRequest.id_local);
            if (local == null)
            {
                return NotFound();
            }

            local.local = updateRequest.local;
            local.id_cliente = updateRequest.id_cliente;
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
        public async Task<IActionResult> Delete(Local deleteRequest)
        {
            var local = await contexto.Local.FindAsync(deleteRequest.id_local);

            if (local == null)
            {
                return NotFound();
            }
            contexto.Local.Remove(local);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */
    }
}

