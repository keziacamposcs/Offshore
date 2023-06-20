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
    public class OrdemCompraController : Controller
    {
        private readonly Contexto contexto;

        public OrdemCompraController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ordemCompras = await contexto.OrdemCompra.ToListAsync();
            return View(ordemCompras);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("oc,valor, valor, data_compra")] OrdemCompra createRequest)
        {
            var ordemCompra = new OrdemCompra
            {
                oc = createRequest.oc,
                valor = createRequest.valor,
                data_compra = createRequest.data_compra
            };

            contexto.OrdemCompra.Add(ordemCompra);
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
            var ordemCompra = await contexto.OrdemCompra.FirstOrDefaultAsync(x => x.id_oc == id);
            return View(ordemCompra);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ordemCompra = await contexto.OrdemCompra.FirstOrDefaultAsync(x => x.id_oc == id);
            if (ordemCompra == null)
            {
                return NotFound();
            }
            return View(ordemCompra);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrdemCompra updateRequest)
        {
            var ordemCompra = await contexto.OrdemCompra.FindAsync(updateRequest.id_oc);
            if (ordemCompra == null)
            {
                return NotFound();
            }

            ordemCompra.oc = updateRequest.oc;
            ordemCompra.valor = updateRequest.valor;
            ordemCompra.data_compra = updateRequest.data_compra;
            
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
        public async Task<IActionResult> Delete(OrdemCompra deleteRequest)
        {
            var ordemCompra = await contexto.OrdemCompra.FindAsync(deleteRequest.id_oc);

            if (ordemCompra == null)
            {
                return NotFound();
            }
            contexto.OrdemCompra.Remove(ordemCompra);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */
    }
}

