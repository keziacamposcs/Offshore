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
    public class ClienteController : Controller
    {
        private readonly Contexto _contexto;

        public ClienteController(Contexto contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clientes = await _contexto.Cliente.ToListAsync();
            return View(clientes);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("cliente,cnpj")] Cliente createRequest)
        {
            var cliente = new Cliente
            {
                cliente = createRequest.cliente,
                cnpj = createRequest.cnpj
            };

            _contexto.Cliente.Add(cliente);
            try
            {
                await _contexto.SaveChangesAsync();
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
            var cliente = await _contexto.Cliente.FirstOrDefaultAsync(x => x.id_cliente == id);
            return View(cliente);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _contexto.Cliente.FirstOrDefaultAsync(x => x.id_cliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Cliente updateRequest)
        {
            var cliente = await _contexto.Cliente.FindAsync(updateRequest.id_cliente);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.cliente = updateRequest.cliente;
            cliente.cnpj = updateRequest.cnpj;

            try
            {
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Fim - Update


        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete (Cliente deleteRequest)
        {
            var cliente = await _contexto.Cliente.FindAsync(deleteRequest.id_cliente);
            if(cliente == null)
            {
                return NotFound();
            }
            _contexto.Cliente.Remove(cliente);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */
    }
}

