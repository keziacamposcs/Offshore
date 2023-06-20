using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace OffshoreTrack.Controllers
{
    public class ParteSoltaController : Controller
    {
        private readonly Contexto contexto;

        public ParteSoltaController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var parteSolta = await contexto.ParteSolta.ToListAsync();
            return View(parteSolta);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("partesolta,quantidade, anexo, id_oc, id_material")] ParteSolta createRequest)
        {
            var parteSolta = new ParteSolta
            {
                partesolta = createRequest.partesolta,
                quantidade = createRequest.quantidade,
                anexo = createRequest.anexo,
                id_oc = createRequest.id_oc,
                id_material = createRequest.id_material
            };

            contexto.ParteSolta.Add(parteSolta);
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
            var parteSolta = await contexto.ParteSolta.FirstOrDefaultAsync(x => x.id_partesolta == id);
            return View(parteSolta);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var parteSolta = await contexto.ParteSolta.FirstOrDefaultAsync(x => x.id_partesolta == id);
            if (parteSolta == null)
            {
                return NotFound();
            }
            return View(parteSolta);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ParteSolta updateRequest)
        {
            var parteSolta = await contexto.ParteSolta.FindAsync(updateRequest.id_partesolta);
            if (parteSolta == null)
            {
                return NotFound();
            }

            parteSolta.partesolta = updateRequest.partesolta;
            parteSolta.quantidade = updateRequest.quantidade;
            parteSolta.anexo = updateRequest.anexo;
            parteSolta.id_oc = updateRequest.id_oc;
            parteSolta.id_material = updateRequest.id_material;
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
        public async Task<IActionResult> Delete(ParteSolta deleteRequest)
        {
            var parteSolta = await contexto.ParteSolta.FindAsync(deleteRequest.id_partesolta);

            if (parteSolta == null)
            {
                return NotFound();
            }
            contexto.ParteSolta.Remove(parteSolta);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */

    }
}

