using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.AspNetCore.Authorization;


namespace OffshoreTrack.Controllers
{
    [Authorize]
    public class CriticidadeController : Controller
    {
        private readonly Contexto contexto;

        public CriticidadeController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var temPermissao = User.HasClaim("PermissaoCriticidade", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var criticidades = await contexto.Criticidade.Where(c => c.Deletado != true).ToListAsync();
            return View(criticidades);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var temPermissao = User.HasClaim("PermissaoCriticidade", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeCriar = User.HasClaim("PodeCriar", "True");
            if(!podeCriar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("criticidade")] Criticidade createRequest)
        {
            var temPermissao = User.HasClaim("PermissaoCriticidade", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var criticidade = new Criticidade
            {
                criticidade = createRequest.criticidade
            };

            contexto.Criticidade.Add(criticidade);
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
            var temPermissao = User.HasClaim("PermissaoCriticidade", "True");
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

            var criticidade = await contexto.Criticidade.FirstOrDefaultAsync(x => x.id_criticidade == id);
            return View(criticidade);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var temPermissao = User.HasClaim("PermissaoCriticidade", "True");
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
            var criticidade = await contexto.Criticidade.FirstOrDefaultAsync(x => x.id_criticidade == id);
            if (criticidade == null)
            {
                return NotFound();
            }
            return View(criticidade);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Criticidade updateRequest)
        {
            var criticidade = await contexto.Criticidade.FindAsync(updateRequest.id_criticidade);
            if (criticidade == null)
            {
                return NotFound();
            }
            criticidade.criticidade = updateRequest.criticidade;
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
        public async Task<IActionResult> Delete(Criticidade deleteRequest)
        {
            var temPermissao = User.HasClaim("PermissaoCriticidade", "True");
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

            var criticidade = await contexto.Criticidade.FindAsync(deleteRequest.id_criticidade);
            if (criticidade == null)
            {
                return NotFound();
            }
            criticidade.Deletado = true;
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

