using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.AspNetCore.Authorization;


namespace OffshoreTrack.Controllers
{
    [Authorize]
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
            var locais = await contexto.Local.Include(l => l.Cliente).ToListAsync();
            return View(locais);
        }
        /* CRUD */

        // Create


        [HttpGet]
        public IActionResult New()
        {
            var podeCriar = User.HasClaim("PodeCriar", "True");
            if(!podeCriar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var clientes = contexto.Cliente.ToList();

            if (!clientes.Any())
            {
                throw new Exception("Não existem clientes na base de dados!");
            }

            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");

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
            var podeLer = User.HasClaim("PodeLer", "True");
            if(!podeLer)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var local = await contexto.Local.Include(l => l.Cliente).FirstOrDefaultAsync(x => x.id_local == id);
            if (local == null)
            {
                return NotFound();
            }
            return View(local);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var podeAtualizar = User.HasClaim("PodeAtualizar", "True");
            if(!podeAtualizar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var local = await contexto.Local.Include(l => l.Cliente).FirstOrDefaultAsync(x => x.id_local == id);
            if (local == null)
            {
                return NotFound();
            }
            // Obtenha todos os clientes e crie uma lista de seleção.
            var clientes = await contexto.Cliente.ToListAsync();
            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente", local.id_cliente);
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
            var podeDeletar = User.HasClaim("PodeDeletar", "True");
            if(!podeDeletar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

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

