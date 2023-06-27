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
    public class ContratoController : Controller
    {
        private readonly Contexto contexto;

        public ContratoController (Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contrato = await contexto.Contrato.Include(c => c.status).ToListAsync();
            return View(contrato);
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

            var fornecedors = contexto.Fornecedor.ToList();
            var clientes = contexto.Cliente.ToList();
            var status = contexto.Status.ToList();
            var setors = contexto.Setor.ToList();

            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");
            ViewBag.status = new SelectList(status, "id_status", "status");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("contrato, dataInicio, dataFim, id_fornecedor, id_cliente, id_status, id_setor,  anexo")] Contrato createRequest, IFormFile anexoFile)
        {
            var contratos = new Contrato
            {
                contrato = createRequest.contrato,
                dataInicio = createRequest.dataInicio,
                dataFim = createRequest.dataFim,
                id_fornecedor = createRequest.id_fornecedor,
                id_cliente = createRequest.id_cliente,
                id_status = createRequest.id_status,
                id_setor = createRequest.id_setor,
                anexo = createRequest.anexo
            };

            if (anexoFile != null && anexoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    contratos.anexo = memoryStream.ToArray();
                }
            }
            else
            {
                contratos.anexo = null;
            }
            contexto.Contrato.Add(contratos);
            await contexto.SaveChangesAsync();

            return RedirectToAction("Read", new { id = contratos.id_contrato });
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

            var contrato = await contexto.Contrato
                .Include(x => x.fornecedor)
                .Include(x => x.cliente)
                .Include(x => x.status)
                .Include(x => x.setor)
                .FirstOrDefaultAsync(x => x.id_contrato == id);
            if (contrato == null)
            {
                return NotFound();
            }
            else
            {
                return View(contrato);
            }
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

            var contrato = await contexto.Contrato.FirstOrDefaultAsync(x => x.id_contrato == id);
            var fornecedors = contexto.Fornecedor.ToList();
            var status = contexto.Status.ToList();
            var setors = contexto.Setor.ToList();
            var clientes = contexto.Cliente.ToList();

            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.status = new SelectList(status, "id_status", "status");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");

            if (contrato == null)
            {
                return NotFound();
            }
            return View(contrato);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Contrato updateRequest, IFormFile anexoFile)
        {
            var contrato = await contexto.Contrato.FindAsync(updateRequest.id_contrato);
            if (contrato == null)
            {
                return NotFound();
            }

            contrato.contrato = updateRequest.contrato;
            contrato.dataInicio = updateRequest.dataInicio;
            contrato.dataFim = updateRequest.dataFim;
            contrato.id_fornecedor = updateRequest.id_fornecedor;
            contrato.id_cliente = updateRequest.id_cliente;
            contrato.id_status = updateRequest.id_status;
            contrato.id_setor = updateRequest.id_setor;

            if (anexoFile != null)
            {
                contrato.anexo = null;

                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    contrato.anexo = memoryStream.ToArray();
                }
            }
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
        public async Task<IActionResult> Delete(Contrato deleteRequest)
        {
            var podeDeletar = User.HasClaim("PodeDeletar", "True");
            if(!podeDeletar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var contrato = await contexto.Contrato.FindAsync(deleteRequest.id_contrato);

            if (contrato == null)
            {
                return NotFound();
            }
            contexto.Contrato.Remove(contrato);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */

        [HttpGet]
        public async Task<IActionResult> DownloadAnexo(int id)
        {
            var contrato = await contexto.Contrato.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }

            if (contrato.anexo == null || contrato.anexo.Length == 0)
            {
                return NotFound("O anexo não está disponível.");
            }

            // Define o tipo MIME do arquivo
            string contentType = "application/octet-stream";

            // Define o nome do arquivo para download (opcional)
            string fileName = "anexo.pdf";

            // Retorna o arquivo como um resultado para download
            return File(contrato.anexo, contentType, fileName);
        }


    }
}