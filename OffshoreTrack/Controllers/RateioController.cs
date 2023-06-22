using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OffshoreTrack.Controllers
{
    public class RateioController : Controller
    { 
        private readonly Contexto contexto;

        public RateioController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rateio = await contexto.Rateio.ToListAsync();
            return View(rateio);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var setores1 = contexto.Setor.ToList();
            var setores2 = contexto.Setor.ToList();

            if (!setores1.Any())
            {
                throw new Exception("Não existem Setores na base de dados!");
            }

            ViewBag.setores1 = new SelectList(setores1, "id_setor", "setor");
            ViewBag.setores2 = new SelectList(setores2, "id_setor", "setor");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("rateio,valor1, valor2, id_setor1, id_setor2")] Rateio createRequest)
        {
            var rateio = new Rateio
            {
                rateio = createRequest.rateio,
                valor1 = createRequest.valor1,
                valor2 = createRequest.valor2,
                id_setor1 = createRequest.id_setor1,
                id_setor2 = createRequest.id_setor2
            };

            contexto.Rateio.Add(rateio);
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
            var rateio = await contexto.Rateio
                            .Include(r => r.setor1)
                            .Include(r => r.setor2)
                            .FirstOrDefaultAsync(x => x.id_rateio == id);

            return View(rateio);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var rateio = await contexto.Rateio
                            .Include(r => r.setor1)
                            .Include(r => r.setor2)
                            .FirstOrDefaultAsync(x => x.id_rateio == id);
            if (rateio == null)
            {
                return NotFound();
            }
            var setores1 = contexto.Setor.ToList();
            var setores2 = contexto.Setor.ToList();
            ViewBag.setores1 = new SelectList(setores1, "id_setor", "setor");
            ViewBag.setores2 = new SelectList(setores2, "id_setor", "setor");
            return View(rateio);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Rateio updateRequest)
        {
            var rateio = await contexto.Rateio.FindAsync(updateRequest.id_rateio);
            if (rateio == null)
            {
                return NotFound();
            }

            rateio.rateio = updateRequest.rateio;

            rateio.id_setor1 = updateRequest.id_setor1;
            rateio.valor1 = updateRequest.valor1;

            rateio.id_setor2 = updateRequest.id_setor2;
            rateio.valor2 = updateRequest.valor2;
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
        public async Task<IActionResult> Delete(Rateio deleteRequest)
        {
            var rateio = await contexto.Rateio.FindAsync(deleteRequest.id_rateio);

            if (rateio == null)
            {
                return NotFound();
            }
            contexto.Rateio.Remove(rateio);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */

    }
}

