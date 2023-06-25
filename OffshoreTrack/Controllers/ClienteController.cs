using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;

namespace OffshoreTrack.Controllers
{
    public class ClienteController : Controller
    {
        private readonly Contexto contexto;

        public ClienteController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clientes = await contexto.Cliente.ToListAsync();
            return View(clientes);
        }

        /* CRUD */

        // Create
        [HttpGet]
        public IActionResult New()
        {
            var clientes = contexto.Cliente.ToList();

            if (clientes == null || !clientes.Any())
            {
                Console.WriteLine("Não foi possível obter clientes do banco de dados");
            }

            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("cliente, razaoSocial, cnpj, endereco, telefone")] Cliente createRequest)
        {
            var cliente = new Cliente
            {
                cliente = createRequest.cliente,
                razaoSocial = createRequest.razaoSocial,
                cnpj = createRequest.cnpj,
                endereco = createRequest.endereco,
                telefone = createRequest.telefone
            };

            contexto.Cliente.Add(cliente);
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
            var cliente = await contexto.Cliente.FirstOrDefaultAsync(x => x.id_cliente == id);
            return View(cliente);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await contexto.Cliente.FirstOrDefaultAsync(x => x.id_cliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Cliente updateRequest)
        {
            var cliente = await contexto.Cliente.FindAsync(updateRequest.id_cliente);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.cliente = updateRequest.cliente;
            cliente.razaoSocial = updateRequest.razaoSocial;
            cliente.cnpj = updateRequest.cnpj;
            cliente.endereco = updateRequest.endereco;
            cliente.telefone = updateRequest.telefone;

            try
            {
                await contexto.SaveChangesAsync();
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
            var cliente = await contexto.Cliente.FindAsync(deleteRequest.id_cliente);
            if(cliente == null)
            {
                return NotFound();
            }
            contexto.Cliente.Remove(cliente);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */
    }
}

