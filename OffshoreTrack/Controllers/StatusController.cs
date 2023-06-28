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
    public class StatusController : Controller
    {

        private readonly Contexto contexto;

        public StatusController (Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var status = await contexto.Status.Where(c => c.Deletado != true).ToListAsync();
            return View(status);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("status")] Status createRequest)
        {
            var status = new Status
            {
                status = createRequest.status
            };

            contexto.Status.Add(status);
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
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var status = await contexto.Status.FirstOrDefaultAsync(x => x.id_status == id);
            return View(status);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var status = await contexto.Status.FirstOrDefaultAsync(x => x.id_status == id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

       [HttpPost]
        public async Task<IActionResult> Update(Status updateRequest)
        {
            var status = await contexto.Status.FindAsync(updateRequest.id_status);
            if (status == null)
            {
                return NotFound();
            }

            status.status = updateRequest.status;
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
        public async Task<IActionResult> Delete(Status deleteRequest)
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var status = await contexto.Status.FindAsync(deleteRequest.id_status);

            if (status == null)
            {
                return NotFound();
            }
            status.Deletado = true;
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
        // Fim - Delete

        /* Fim - CRUD */

    }
}