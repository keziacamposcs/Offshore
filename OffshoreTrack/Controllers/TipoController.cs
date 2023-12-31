﻿using System;
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
    public class TipoController : Controller
    {

        private readonly Contexto contexto;

        public TipoController (Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var temPermissao = User.HasClaim("PermissaoTipo", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var tipo = await contexto.Tipo.Where(c => c.Deletado != true).ToListAsync();
            return View(tipo);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {   
            var temPermissao = User.HasClaim("PermissaoTipo", "True");
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
        public async Task<IActionResult> Create([Bind("tipo")] Tipo createRequest)
        {
            var tipo = new Tipo
            {
                tipo = createRequest.tipo
            };

            contexto.Tipo.Add(tipo);
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
            var temPermissao = User.HasClaim("PermissaoTipo", "True");
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

            var tipo = await contexto.Tipo.FirstOrDefaultAsync(x => x.id_tipo == id);
            return View(tipo);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var temPermissao = User.HasClaim("PermissaoTipo", "True");
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


            var tipo = await contexto.Tipo.FirstOrDefaultAsync(x => x.id_tipo == id);
            if (tipo == null)
            {
                return NotFound();
            }
            return View(tipo);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Tipo updateRequest)
        {
            var tipo = await contexto.Tipo.FindAsync(updateRequest.id_tipo);
            if (tipo == null)
            {
                return NotFound();
            }

            tipo.tipo = updateRequest.tipo;
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
        public async Task<IActionResult> Delete(Tipo deleteRequest)
        {   
            var temPermissao = User.HasClaim("PermissaoTipo", "True");
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

            var tipo = await contexto.Tipo.FindAsync(deleteRequest.id_tipo);

            if (tipo == null)
            {
                return NotFound();
            }

            tipo.Deletado = true;
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