﻿using System;
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
    public class FornecedorController : Controller
    {
        private readonly Contexto contexto;

        public FornecedorController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var temPermissao = User.HasClaim("PermissaoFornecedor", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var fornecedores = await contexto.Fornecedor.Where(c => c.Deletado != true).ToListAsync();
            return View(fornecedores);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var temPermissao = User.HasClaim("PermissaoFornecedor", "True");
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
        public async Task<IActionResult> Create([Bind("fornecedor,razaoSocial, cnpj, endereco, telefone, email, vendedor, inscricaoEstadual, inscricaoMunicipal")] Fornecedor createRequest)
        {
            var temPermissao = User.HasClaim("PermissaoFornecedor", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var fornecedor = new Fornecedor
            {
                fornecedor = createRequest.fornecedor,
                razaoSocial = createRequest.razaoSocial,
                cnpj = createRequest.cnpj,
                endereco = createRequest.endereco,
                telefone = createRequest.telefone,
                email = createRequest.email,
                vendedor = createRequest.vendedor,
                inscricaoEstadual = createRequest.inscricaoEstadual,
                inscricaoMunicipal = createRequest.inscricaoMunicipal

            };

            contexto.Fornecedor.Add(fornecedor);
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
            var temPermissao = User.HasClaim("PermissaoFornecedor", "True");
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

            var fornecedor = await contexto.Fornecedor.FirstOrDefaultAsync(x => x.id_fornecedor == id);
            return View(fornecedor);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var temPermissao = User.HasClaim("PermissaoFornecedor", "True");
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

            var fornecedor = await contexto.Fornecedor.FirstOrDefaultAsync(x => x.id_fornecedor == id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Fornecedor updateRequest)
        {
            var fornecedor = await contexto.Fornecedor.FindAsync(updateRequest.id_fornecedor);
            if (fornecedor == null)
            {
                return NotFound();
            }

            fornecedor.fornecedor = updateRequest.fornecedor;
            fornecedor.razaoSocial = updateRequest.razaoSocial;
            fornecedor.cnpj = updateRequest.cnpj;
            fornecedor.endereco = updateRequest.endereco;
            fornecedor.telefone = updateRequest.telefone;
            fornecedor.email = updateRequest.email;
            fornecedor.vendedor = updateRequest.vendedor;
            fornecedor.inscricaoEstadual = updateRequest.inscricaoEstadual;
            fornecedor.inscricaoMunicipal = updateRequest.inscricaoMunicipal;

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
        public async Task<IActionResult> Delete(Fornecedor deleteRequest)
        {
            var temPermissao = User.HasClaim("PermissaoFornecedor", "True");
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

            var fornecedor = await contexto.Fornecedor.FindAsync(deleteRequest.id_fornecedor);

            if (fornecedor == null)
            {
                return NotFound();
            }

            fornecedor.Deletado = true;
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

