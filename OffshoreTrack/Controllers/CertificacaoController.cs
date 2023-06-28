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
    public class CertificacaoController : Controller
    {

        private readonly Contexto contexto;

        public CertificacaoController (Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var temPermissao = User.HasClaim("PermissaoCertificado", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var certificacao = await contexto.Certificacao.Where(c => c.Deletado != true).ToListAsync();
            return View(certificacao);
        }

        /* CRUD */

        // Create

         [HttpGet]
        public IActionResult New()
        {   
            var temPermissao = User.HasClaim("PermissaoCertificado", "True");
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
        public async Task<IActionResult> Create([Bind("certificacao, orgaoEmissor, dataEmissao, dataValidade, anexo")] Certificacao createRequest, IFormFile anexoFile)
        {
            var certificacao = new Certificacao
            {
                certificacao = createRequest.certificacao,
                orgaoEmissor = createRequest.orgaoEmissor,
                dataEmissao = createRequest.dataEmissao,
                dataValidade = createRequest.dataValidade,
                anexo = createRequest.anexo
            };

            if (anexoFile != null && anexoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    certificacao.anexo = memoryStream.ToArray();
                }
            }
            contexto.Certificacao.Add(certificacao);
            
            try{
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
            var temPermissao = User.HasClaim("PermissaoCertificado", "True");
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

            var certificacao = await contexto.Certificacao
            .FirstOrDefaultAsync(x => x.id_certificacao == id);

            return View(certificacao);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var temPermissao = User.HasClaim("PermissaoCertificado", "True");
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

            var certificacao = await contexto.Certificacao.FirstOrDefaultAsync(x => x.id_certificacao == id);
            if (certificacao == null)
            {
                return NotFound();
            }
            return View(certificacao);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Certificacao updateRequest, IFormFile anexoFile)
        {
            var certificacao = await contexto.Certificacao.FindAsync(updateRequest.id_certificacao);
            if (certificacao == null)
            {
                return NotFound();
            }

            certificacao.certificacao = updateRequest.certificacao;
            certificacao.orgaoEmissor = updateRequest.orgaoEmissor;
            certificacao.dataEmissao = updateRequest.dataEmissao;
            certificacao.dataValidade = updateRequest.dataValidade;

            if (anexoFile != null)
            {
                certificacao.anexo = null;

                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    certificacao.anexo = memoryStream.ToArray();
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
        public async Task<IActionResult> Delete(Certificacao deleteRequest)
        {
            var temPermissao = User.HasClaim("PermissaoCertificado", "True");
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

            var certificacao = await contexto.Certificacao.FindAsync(deleteRequest.id_certificacao);

            if (certificacao == null)
            {
                return NotFound();
            }

            certificacao.Deletado = true;
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

        [HttpGet]
        public async Task<IActionResult> DownloadAnexo(int id)
        {
            var certificacao = await contexto.Certificacao.FindAsync(id);
            if (certificacao == null)
            {
                return NotFound();
            }

            if (certificacao.anexo == null || certificacao.anexo.Length == 0)
            {
                return NotFound("O anexo não está disponível.");
            }

            // Define o tipo MIME do arquivo
            string contentType = "application/octet-stream";

            // Define o nome do arquivo para download (opcional)
            string fileName = "anexo.pdf";

            // Retorna o arquivo como um resultado para download
            return File(certificacao.anexo, contentType, fileName);
        }


    }
}