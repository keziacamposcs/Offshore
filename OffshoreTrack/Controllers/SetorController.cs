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
    public class SetorController : Controller
    {
        private readonly Contexto contexto;

        public SetorController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var setor = await contexto.Setor.ToListAsync();
            return View(setor);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("setor")] Setor createRequest)
        {
            var setor = new Setor
            {
                setor = createRequest.setor
            };

            contexto.Setor.Add(setor);
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
            var setor = await contexto.Setor.FirstOrDefaultAsync(x => x.id_setor == id);
            return View(setor);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var setor = await contexto.Setor.FirstOrDefaultAsync(x => x.id_setor == id);
            if (setor == null)
            {
                return NotFound();
            }
            return View(setor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Setor updateRequest)
        {
            var setor = await contexto.Setor.FindAsync(updateRequest.id_setor);
            if (setor == null)
            {
                return NotFound();
            }

            setor.setor = updateRequest.setor;
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
        public async Task<IActionResult> Delete(Setor deleteRequest)
        {
            var setor = await contexto.Setor.FindAsync(deleteRequest.id_setor);

            if (setor == null)
            {
                return NotFound();
            }
            contexto.Setor.Remove(setor);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */

    }
}

