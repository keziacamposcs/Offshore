using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace OffshoreTrack.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create([Bind("rateio,porcentagem1, porcentagem2, id_setor1, id_setor2")] Rateio createRequest)
        {
            if (createRequest.porcentagem1 < 0 || createRequest.porcentagem1 > 100 || 
                createRequest.porcentagem2 < 0 || createRequest.porcentagem2 > 100)
            {
                // Retornar um erro caso os valores estejam fora da faixa aceitável
                return BadRequest("Valores inválidos. Entre 0% e 100%");
            }

            var rateio = new Rateio
            {
                rateio = createRequest.rateio,
                porcentagem1 = createRequest.porcentagem1,
                porcentagem2 = createRequest.porcentagem2,
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

    var setores1 = await contexto.Setor.ToListAsync();
    var setores2 = await contexto.Setor.ToListAsync();
    ViewBag.setores1 = new SelectList(setores1, "id_setor", "setor", rateio.id_setor1);
    ViewBag.setores2 = new SelectList(setores2, "id_setor", "setor", rateio.id_setor2);
    return View(rateio);
}

[HttpPost]
public async Task<IActionResult> Update([Bind("id_rateio, rateio, porcentagem1, porcentagem2, id_setor1, id_setor2")] Rateio updateRequest)
{
    if (!ModelState.IsValid)
    {
        var setores = contexto.Setor.ToList();
        ViewBag.setores1 = new SelectList(setores, "id_setor", "setor", updateRequest.id_setor1);
        ViewBag.setores2 = new SelectList(setores, "id_setor", "setor", updateRequest.id_setor2);

        return View("Edit", updateRequest);
    }

    var rateio = await contexto.Rateio.FindAsync(updateRequest.id_rateio);
    if (rateio == null)
    {
        return NotFound();
    }

    if (updateRequest.porcentagem1 < 0 || updateRequest.porcentagem1 > 100 || 
        updateRequest.porcentagem2 < 0 || updateRequest.porcentagem2 > 100)
    {
        // Retornar um erro caso os valores estejam fora da faixa aceitável
        return BadRequest("Valores inválidos. Entre 0% e 100%");
    }

    rateio.rateio = updateRequest.rateio;
    rateio.porcentagem1 = updateRequest.porcentagem1;
    rateio.id_setor1 = updateRequest.id_setor1;
    rateio.porcentagem2 = updateRequest.porcentagem2;
    rateio.id_setor2 = updateRequest.id_setor2;

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

