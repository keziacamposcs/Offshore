﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OffshoreTrack.Controllers
{
    [Authorize]
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
            var temPermissao = User.HasClaim("PermissaoParteSolta", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var parteSolta = await contexto.ParteSolta
                .Include(x => x.fornecedor)
                .Include(x => x.material)
                .Where(c => c.Deletado != true)
                .ToListAsync();

            return View(parteSolta);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var temPermissao = User.HasClaim("PermissaoParteSolta", "True");
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

            var ordemCompras = contexto.OrdemCompra.Where(c => c.Deletado != true).ToList();
            var materiais = contexto.Material.Where(c => c.Deletado != true).ToList();
            var fornecedors = contexto.Fornecedor.Where(c => c.Deletado != true).ToList();
            var status = contexto.Status.Where(c => c.Deletado != true).ToList();
            var locais = contexto.Local.Where(c => c.Deletado != true).ToList();
            var certificacaos = contexto.Certificacao.Where(c => c.Deletado != true).ToList();

            ViewBag.ordemCompras = new SelectList(ordemCompras, "id_oc", "oc");
            ViewBag.materiais = new SelectList(materiais, "id_material", "material");
            ViewBag.fornecedors = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.status = new SelectList(status, "id_status", "status");
            ViewBag.locais = new SelectList(locais, "id_local", "local");
            ViewBag.certificacaos = new SelectList(certificacaos, "id_certificacao", "certificacao");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("partesolta, descricao, dimensoes, peso, quantidade, anexo, id_oc, id_material, id_fornecedor, id_status, id_local, id_certificacao")] ParteSolta createRequest, IFormFile anexoFile)
        {
            var temPermissao = User.HasClaim("PermissaoParteSolta", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var parteSolta = new ParteSolta
            {
                partesolta = createRequest.partesolta,
                descricao = createRequest.descricao,
                dimensoes = createRequest.dimensoes,
                peso = createRequest.peso,
                quantidade = createRequest.quantidade,
                anexo = createRequest.anexo,
                id_oc = createRequest.id_oc,
                id_material = createRequest.id_material,
                id_fornecedor = createRequest.id_fornecedor,
                id_status = createRequest.id_status,
                id_local = createRequest.id_local,
                id_certificacao = createRequest.id_certificacao
            };

            if (anexoFile != null && anexoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    parteSolta.anexo = memoryStream.ToArray();
                }
            }
            else
            {
                parteSolta.anexo = null; 
            }
            contexto.ParteSolta.Add(parteSolta);
            await contexto.SaveChangesAsync();

            var atividadeLogPSCreate = new AtividadeLogPS
            {
                id_parteSolta = parteSolta.id_partesolta,
                Timestamp = DateTime.Now,
                acao = "Parte Solta criado"
            };

            await LogAtividadePS(atividadeLogPSCreate);

            return RedirectToAction("Read", new { id = parteSolta.id_partesolta });
        }

        // Fim - Create

        // Read
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var temPermissao = User.HasClaim("PermissaoParteSolta", "True");
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

            var parteSolta = await contexto.ParteSolta
                .Include(x => x.oc)
                .Include(x => x.fornecedor)
                .Include(x => x.material)
                .Include(x => x.local)
                .Include(x => x.certificacao)
                .Include(x => x.status)
                .Include(x => x.atividadeLogsPS)
                .FirstOrDefaultAsync(x => x.id_partesolta == id);

            if (parteSolta == null)
            {
                return NotFound();
            }
            else
            {
                return View(parteSolta);
            }
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var temPermissao = User.HasClaim("PermissaoParteSolta", "True");
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

            var parteSolta = await contexto.ParteSolta.FirstOrDefaultAsync(x => x.id_partesolta == id);
            var locals = contexto.Local.Where(c => c.Deletado != true).ToList();
            var status = contexto.Status.Where(c => c.Deletado != true).ToList();
            var certificacaos = contexto.Certificacao.Where(c => c.Deletado != true).ToList();
            var fornecedors = contexto.Fornecedor.Where(c => c.Deletado != true).ToList();
            var materiais = contexto.Material.Where(c => c.Deletado != true).ToList();
            var ordemCompras = contexto.OrdemCompra.Where(c => c.Deletado != true).ToList();


            ViewBag.local = new SelectList(locals, "id_local", "local");
            ViewBag.status = new SelectList(status, "id_status", "status");
            ViewBag.certificacao = new SelectList(certificacaos, "id_certificacao", "certificacao");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.material = new SelectList(materiais, "id_material", "material");
            ViewBag.oc = new SelectList(ordemCompras, "id_oc", "oc");

            if (parteSolta == null)
            {
                return NotFound();
            }
            return View(parteSolta);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ParteSolta updateRequest, IFormFile anexoFile)
        {
            var parteSolta = await contexto.ParteSolta.FindAsync(updateRequest.id_partesolta);
            if (parteSolta == null)
            {
                return NotFound();
            }

            parteSolta.id_material = updateRequest.id_material;
            parteSolta.id_fornecedor = updateRequest.id_fornecedor;
            parteSolta.id_oc = updateRequest.id_oc;
            parteSolta.id_local = updateRequest.id_local;
            parteSolta.id_certificacao = updateRequest.id_certificacao;
            parteSolta.id_status = updateRequest.id_status;
        
            if (anexoFile != null)
            {
                parteSolta.anexo = null;
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    parteSolta.anexo = memoryStream.ToArray();
                }
            }
            contexto.ParteSolta.Update(parteSolta);
            await contexto.SaveChangesAsync();

            var atividadeLogPSCreate = new AtividadeLogPS
            {
                id_parteSolta = parteSolta.id_partesolta,
                Timestamp = DateTime.Now,
                acao = "Material Atualizado",
            };
            await LogAtividadePS(atividadeLogPSCreate);
            return RedirectToAction("Read", new { id = parteSolta.id_partesolta });
        }

        // Fim - Update

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var temPermissao = User.HasClaim("PermissaoParteSolta", "True");
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

            var parteSolta = await contexto.ParteSolta.FindAsync(id);

            if (parteSolta == null)
            {
                return NotFound();
            }
            parteSolta.Deletado = true;
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

        // Log de Atividade
        public async Task LogAtividadePS(AtividadeLogPS atividadeLogPS)
        {
            if (atividadeLogPS != null)
            {
                contexto.AtividadeLogPS.Add(atividadeLogPS);
                await contexto.SaveChangesAsync();
            }
        }
        // Fim - Log de Atividade

        // Download de Anexo
        [HttpGet]
        public async Task<IActionResult> DownloadAnexo(int id)
        {
            var partesoltas = await contexto.ParteSolta.FindAsync(id);
            
            if (partesoltas == null)
            {
                return NotFound();
            }

            if (partesoltas.anexo == null || partesoltas.anexo.Length == 0)
            {
                return NotFound("O anexo não está disponível.");
            }

            // Define o tipo MIME do arquivo
            string contentType = "application/octet-stream";

            // Define o nome do arquivo para download (opcional)
            string fileName = "anexo.pdf";

            // Retorna o arquivo como um resultado para download
            return File(partesoltas.anexo, contentType, fileName);
        }
        // Fim - Download de Anexo

    }
}

