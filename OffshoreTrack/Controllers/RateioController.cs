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
            var temPermissao = User.HasClaim("PermissaoRateio", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var rateio = await contexto.Rateio.Where(c => c.Deletado != true).ToListAsync();
            return View(rateio);
        }

        /* CRUD */

        // Create
        [HttpGet]
        public IActionResult New()
        {
            var temPermissao = User.HasClaim("PermissaoRateio", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeCriar = User.HasClaim("PodeCriar", "True");
            if (!podeCriar)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var setores1 = contexto.Setor.Where(c => c.Deletado != true).ToList();
            var setores2 = contexto.Setor.Where(c => c.Deletado != true).ToList();

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
            var temPermissao = User.HasClaim("PermissaoRateio", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            if (createRequest.porcentagem1.HasValue && createRequest.porcentagem2.HasValue && 
                createRequest.porcentagem1 + createRequest.porcentagem2 != 100)
            {
                // Add model error
                ModelState.AddModelError("porcentagem1", "A soma das porcentagens deve ser 100%");
                ModelState.AddModelError("porcentagem2", "A soma das porcentagens deve ser 100%");
            }

            if (ModelState.IsValid)
            {
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

            // If we got this far, something failed, redisplay form
            var setores1 = await contexto.Setor.ToListAsync();
            var setores2 = await contexto.Setor.ToListAsync();
            ViewBag.setores1 = new SelectList(setores1, "id_setor", "setor");
            ViewBag.setores2 = new SelectList(setores2, "id_setor", "setor");

            return View(createRequest);
        }

        // Fim - Create


        // Fim - Create

        // Read
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var temPermissao = User.HasClaim("PermissaoRateio", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeLer = User.HasClaim("PodeLer", "True");
            if(!podeLer)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

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
            var temPermissao = User.HasClaim("PermissaoRateio", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeAtualizar = User.HasClaim("PodeAtualizar", "True");
            if(!podeAtualizar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var rateio = await contexto.Rateio.Include(r => r.setor1)
                                            .Include(r => r.setor2)
                                            .FirstOrDefaultAsync(x => x.id_rateio == id);

            if (rateio == null)
            {
                return NotFound();
            }
            var setores1 = await contexto.Setor.Where(c => c.Deletado != true).ToListAsync();
            var setores2 = await contexto.Setor.Where(c => c.Deletado != true).ToListAsync();
            ViewBag.setores1 = new SelectList(setores1, "id_setor", "setor");
            ViewBag.setores2 = new SelectList(setores2, "id_setor", "setor");

            return View(rateio);
        }

        [HttpPost]
        public async Task<IActionResult>Update(Rateio updateRequest)
        {
            var rateio = await contexto.Rateio.FindAsync(updateRequest.id_rateio);
            if (rateio == null)
            {
                return NotFound();
            }
            rateio.rateio = updateRequest.rateio;
            rateio.porcentagem1 = updateRequest.porcentagem1;
            rateio.porcentagem2 = updateRequest.porcentagem2;
            rateio.id_setor1 = updateRequest.id_setor1;
            rateio.id_setor2 = updateRequest.id_setor2;
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
        public async Task<IActionResult> Delete(Rateio deleteRequest)
        {
            var temPermissao = User.HasClaim("PermissaoRateio", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeDeletar = User.HasClaim("PodeDeletar", "True");
            if(!podeDeletar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var rateio = await contexto.Rateio.FindAsync(deleteRequest.id_rateio);
            if (rateio == null)
            {
                return NotFound();
            }
            rateio.Deletado = true;
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
        // Fim - Delete

        /* Fim - CRUD */

    }
}

