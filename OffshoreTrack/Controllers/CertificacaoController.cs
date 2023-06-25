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
            var certificacao = await contexto.Certificacao.ToListAsync();
            return View(certificacao);
        }

        /* CRUD */

        // Create

         [HttpGet]
        public IActionResult New()
        {
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
        [Authorize(Policy = "PodeLer")]
        public async Task<IActionResult> Read(int id)
        {
            var certificacao = await contexto.Certificacao
            .FirstOrDefaultAsync(x => x.id_certificacao == id);

            return View(certificacao);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
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
            var certificacao = await contexto.Certificacao.FindAsync(deleteRequest.id_certificacao);

            if (certificacao == null)
            {
                return NotFound();
            }
            contexto.Certificacao.Remove(certificacao);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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