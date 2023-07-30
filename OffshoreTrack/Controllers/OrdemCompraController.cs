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
            var temPermissao = User.HasClaim("PermissaoOrdemCompra", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var ordemCompras = await contexto.OrdemCompra
                            .Include(x => x.setor)
                            .Include(x => x.status)
                            .Where(c => c.Deletado != true)
                            .ToListAsync();
            return View(ordemCompras);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var temPermissao = User.HasClaim("PermissaoOrdemCompra", "True");
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

            var model = new OrdemCompra();
            model.data_oc = DateTime.Now;

            var setors = contexto.Setor.Where(c => c.Deletado != true).ToList();
            var fornecedors = contexto.Fornecedor.Where(c => c.Deletado != true).ToList();
            var rateios = contexto.Rateio.Where(c => c.Deletado != true).ToList();
            var formaPagamentos = contexto.FormaPagamento.Where(c => c.Deletado != true).ToList();
            var moedas = contexto.Moeda.Where(c => c.Deletado != true).ToList();

            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.fornecedor2 = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.fornecedor3 = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.rateio = new SelectList(rateios, "id_rateio", "rateio");
            ViewBag.formaPagamento = new SelectList(formaPagamentos, "id_formaPagamento", "formaPagamento");
            ViewBag.moedas = contexto.Moeda.Where(c => c.Deletado != true).Select(m => new { m.id_moeda, m.moeda_descricao, m.simbolo }).ToList();

            return View(model); 
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("oc, prioridade, observacao, data_oc, data_prevista, id_fornecedor, id_fornecedor2, id_fornecedor3, id_setor, id_rateio, id_formaPagamento, id_moeda, total, anexo, Itens")] OrdemCompra createRequest ,IFormFile anexoFile)
        {      
            var temPermissao = User.HasClaim("PermissaoOrdemCompra", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            if (anexoFile != null && anexoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    createRequest.anexo = memoryStream.ToArray();
                }
            }
            else
            {
                createRequest.anexo = null; 
            }
            
            var ordemCompra = new OrdemCompra
            {   id_empresa = 1,
                oc = createRequest.oc,
                moeda = createRequest.moeda,
                prioridade = createRequest.prioridade,
                observacao = createRequest.observacao,
                data_oc = createRequest.data_oc,
                data_prevista = createRequest.data_prevista,
                id_fornecedor = createRequest.id_fornecedor,
                id_fornecedor2 = createRequest.id_fornecedor2,
                id_fornecedor3 = createRequest.id_fornecedor3,
                id_setor = createRequest.id_setor,
                id_rateio = createRequest.id_rateio,
                id_formaPagamento = createRequest.id_formaPagamento,
                id_moeda = createRequest.id_moeda,
                total = createRequest.total,
                anexo = createRequest.anexo
            };
            contexto.Add(createRequest);
            await contexto.SaveChangesAsync();

            if (createRequest.Itens != null)
            {
                foreach (var item in createRequest.Itens)
                {
                    contexto.Add(item);
                }
                try{
                await contexto.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    e.ToString();
                }
            }
            return RedirectToAction("Read", new { id = createRequest.id_oc });
        }

        // Fim - Create

        // Read
[HttpGet]
public async Task<IActionResult> Read(int id)
{
    var temPermissao = User.HasClaim("PermissaoOrdemCompra", "True");
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

    var ordemCompra = await contexto.OrdemCompra
                    .Include(x => x.setor)
                    .Include(x => x.status)
                    .Include(x => x.fornecedor)
                    .Include(x => x.fornecedor2)
                    .Include(x => x.fornecedor3)
                    .Include(x => x.rateio)
                    .Include(x => x.empresa)  
                    .Include(x => x.formaPagamento)  
                    .Include(x => x.moeda)      
                    .Include(x => x.Itens)     
    .FirstOrDefaultAsync(x => x.id_oc == id);
    if(ordemCompra == null)
    {
        TempData["Aviso"] = "Não foi possível encontrar a Ordem de Compra.";
        return RedirectToAction("Index", "Home");
    }

    // Retorna a vista e passa o objeto ordemCompra como um modelo
    return View(ordemCompra);
}

        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {   
            var temPermissao = User.HasClaim("PermissaoOrdemCompra", "True");
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

            var ordemCompra = await contexto.OrdemCompra.FirstOrDefaultAsync(x => x.id_oc == id);
            var setors = contexto.Setor.Where(c => c.Deletado != true).ToList();
            var fornecedors1 = contexto.Fornecedor.Where(c => c.Deletado != true).ToList();
            var fornecedors2 = contexto.Fornecedor.Where(c => c.Deletado != true).ToList();
            var fornecedors3 = contexto.Fornecedor.Where(c => c.Deletado != true).ToList();
            var rateios = contexto.Rateio.Where(c => c.Deletado != true).ToList();
            var formaPagamentos = contexto.FormaPagamento.Where(c => c.Deletado != true).ToList();
            
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
            var temPermissao = User.HasClaim("PermissaoOrdemCompra", "True");
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

            var ordemCompra = await contexto.OrdemCompra.FindAsync(deleteRequest.id_oc);

            if (ordemCompra == null)
            {
                return NotFound();
            }
            ordemCompra.Deletado = true;
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

