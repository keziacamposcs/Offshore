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
    public class OrdemCompraController : Controller
    {
        private readonly Contexto contexto;

        public OrdemCompraController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ordemCompras = await contexto.OrdemCompra
                            .Include(x => x.setor)
                            .Include(x => x.status)
                            .ToListAsync();
            return View(ordemCompras);
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

            var model = new OrdemCompra();
            model.data_oc = DateTime.Now;

            var setors = contexto.Setor.ToList();
            var fornecedors1 = contexto.Fornecedor.ToList();
            var fornecedors2 = contexto.Fornecedor.ToList();
            var fornecedors3 = contexto.Fornecedor.ToList();
            var rateios = contexto.Rateio.ToList();
            var formaPagamentos = contexto.FormaPagamento.ToList();

            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.fornecedor = new SelectList(fornecedors1, "id_fornecedor", "fornecedor");
            ViewBag.fornecedor2 = new SelectList(fornecedors2, "id_fornecedor", "fornecedor");
            ViewBag.fornecedor3 = new SelectList(fornecedors3, "id_fornecedor", "fornecedor");
            ViewBag.rateio = new SelectList(rateios, "id_rateio", "rateio");
            ViewBag.formaPagamento = new SelectList(formaPagamentos, "id_formaPagamento", "formaPagamento");
            return View(model); 
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("oc, moeda, prioridade, observacao, data_oc, data_prevista, item1, quantidade1, valor1, item2, quantidade2, valor2, item3,  item4, quantidade4, valor4, item5, quantidade5, valor5, id_fornecedor, id_fornecedor2, id_fornecedor3, id_setor, id_rateio, id_formaPagamento, anexo ")] OrdemCompra createRequest ,IFormFile anexoFile)
        {
            var ordemCompra = new OrdemCompra
            {   id_empresa = 1,
                oc = createRequest.oc,
                moeda = createRequest.moeda,
                prioridade = createRequest.prioridade,
                observacao = createRequest.observacao,
                data_oc = createRequest.data_oc,
                data_prevista = createRequest.data_prevista,
                item1 = createRequest.item1,
                quantidade1 = createRequest.quantidade1,
                valor1 = createRequest.valor1,
                item2 = createRequest.item2,
                quantidade2 = createRequest.quantidade2,
                valor2 = createRequest.valor2,
                item3 = createRequest.item3,
                quantidade3 = createRequest.quantidade3,
                valor3 = createRequest.valor3,
                item4 = createRequest.item4,
                quantidade4 = createRequest.quantidade4,
                valor4 = createRequest.valor4,
                item5 = createRequest.item5,
                quantidade5 = createRequest.quantidade5,
                valor5 = createRequest.valor5,
                id_fornecedor = createRequest.id_fornecedor,
                id_fornecedor2 = createRequest.id_fornecedor2,
                id_fornecedor3 = createRequest.id_fornecedor3,
                id_setor = createRequest.id_setor,
                id_rateio = createRequest.id_rateio,
                id_formaPagamento = createRequest.id_formaPagamento,
                anexo = createRequest.anexo
            };
            if (anexoFile != null && anexoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    ordemCompra.anexo = memoryStream.ToArray();
                }
            }
            else
            {
                ordemCompra.anexo = null; 
            }
            contexto.OrdemCompra.Add(ordemCompra);
            await contexto.SaveChangesAsync();

            return RedirectToAction("Read", new { id = ordemCompra.id_oc });
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

            var ordemCompra = await contexto.OrdemCompra
                            .Include(x => x.setor)
                            .Include(x => x.status)
                            .Include(x => x.fornecedor)
                            .Include(x => x.fornecedor2)
                            .Include(x => x.fornecedor3)
                            .Include(x => x.rateio)
                            .Include(x => x.empresa)  
                            .Include(x => x.formaPagamento)                          
            .FirstOrDefaultAsync(x => x.id_oc == id);
            return View(ordemCompra);
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

            var ordemCompra = await contexto.OrdemCompra.FirstOrDefaultAsync(x => x.id_oc == id);
            var setors = contexto.Setor.ToList();
            var fornecedors1 = contexto.Fornecedor.ToList();
            var fornecedors2 = contexto.Fornecedor.ToList();
            var fornecedors3 = contexto.Fornecedor.ToList();
            var rateios = contexto.Rateio.ToList();
            var formaPagamentos = contexto.FormaPagamento.ToList();
            
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.fornecedor = new SelectList(fornecedors1, "id_fornecedor", "fornecedor");
            ViewBag.fornecedor2 = new SelectList(fornecedors2, "id_fornecedor", "fornecedor");
            ViewBag.fornecedor3 = new SelectList(fornecedors3, "id_fornecedor", "fornecedor");
            ViewBag.rateio = new SelectList(rateios, "id_rateio", "rateio");
            ViewBag.formaPagamento = new SelectList(formaPagamentos, "id_formaPagamento", "formaPagamento");
            
            if (ordemCompra == null)
            {
                return NotFound();
            }
            return View(ordemCompra);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrdemCompra updateRequest, IFormFile anexoFile)
        {
            var ordemCompra = await contexto.OrdemCompra.FindAsync(updateRequest.id_oc);
            if (ordemCompra == null)
            {
                return NotFound();
            }

            ordemCompra.oc = updateRequest.oc;
            ordemCompra.prioridade = updateRequest.prioridade;
            ordemCompra.observacao = updateRequest.observacao;
            ordemCompra.data_oc = updateRequest.data_oc;
            ordemCompra.data_prevista = updateRequest.data_prevista;
            ordemCompra.item1 = updateRequest.item1;
            ordemCompra.quantidade1 = updateRequest.quantidade1;
            ordemCompra.valor1 = updateRequest.valor1;
            ordemCompra.item2 = updateRequest.item2;
            ordemCompra.quantidade2 = updateRequest.quantidade2;
            ordemCompra.valor2 = updateRequest.valor2;
            ordemCompra.item3 = updateRequest.item3;
            ordemCompra.quantidade3 = updateRequest.quantidade3;
            ordemCompra.valor3 = updateRequest.valor3;
            ordemCompra.item4 = updateRequest.item4;
            ordemCompra.quantidade4 = updateRequest.quantidade4;
            ordemCompra.valor4 = updateRequest.valor4;
            ordemCompra.item5 = updateRequest.item5;
            ordemCompra.quantidade5 = updateRequest.quantidade5;
            ordemCompra.valor5 = updateRequest.valor5;
            ordemCompra.id_fornecedor = updateRequest.id_fornecedor;
            ordemCompra.id_fornecedor2 = updateRequest.id_fornecedor2;
            ordemCompra.id_fornecedor3 = updateRequest.id_fornecedor3;
            ordemCompra.id_setor = updateRequest.id_setor;
            ordemCompra.id_rateio = updateRequest.id_rateio;
            ordemCompra.id_formaPagamento = updateRequest.id_formaPagamento;

            if (anexoFile != null && anexoFile.Length > 0)
            {
                ordemCompra.anexo = null;
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    ordemCompra.anexo = memoryStream.ToArray();
                }
            }
            contexto.OrdemCompra.Update(ordemCompra);
            await contexto.SaveChangesAsync();
            return RedirectToAction("Read", new { id = ordemCompra.id_oc });
        }
        // Fim - Update


        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(OrdemCompra deleteRequest)
        {
            var podeDeletar = User.HasClaim("PodeDeletar", "True");
            if(!podeDeletar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var ordemCompra = await contexto.OrdemCompra.FindAsync(deleteRequest.id_oc);

            if (ordemCompra == null)
            {
                return NotFound();
            }
            contexto.OrdemCompra.Remove(ordemCompra);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */

        // Download de Anexo
        [HttpGet]
        public async Task<IActionResult> DownloadAnexo(int id)
        {
            var ordemCompra = await contexto.OrdemCompra.FindAsync(id);
            
            if (ordemCompra == null)
            {
                return NotFound();
            }

            if (ordemCompra.anexo == null || ordemCompra.anexo.Length == 0)
            {
                return NotFound("O anexo não está disponível.");
            }

            // Define o tipo MIME do arquivo
            string contentType = "application/octet-stream";

            // Define o nome do arquivo para download (opcional)
            string fileName = "anexo.pdf";

            // Retorna o arquivo como um resultado para download
            return File(ordemCompra.anexo, contentType, fileName);
        }
        // Fim - Download de Anexo

    }
}

