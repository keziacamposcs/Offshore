using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.AspNetCore.Authorization;

namespace OffshoreTrack.Controllers
{
    [Authorize]
    public class ManutencaoController : Controller
    {
        private readonly Contexto contexto;

        public ManutencaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var temPermissao = User.HasClaim("PermissaoManutencao", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var manutencaos = await contexto.Manutencao
                                            .Include(m => m.material) 
                                            .Include(m => m.setor) 
                                            .Include(m => m.status)
                                            .Where(c => c.Deletado != true)
                                            .ToListAsync();
            return View(manutencaos);
        }


        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var temPermissao = User.HasClaim("PermissaoManutencao", "True");
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


            var materials = contexto.Material.Where(c => c.Deletado != true).ToList();
            var status = contexto.Status.Where(c => c.Deletado != true).ToList();
            var tipos = contexto.Tipo.Where(c => c.Deletado != true).ToList();

            var setors = contexto.Setor.Where(c => c.Deletado != true).ToList();
            var fornecedors = contexto.Fornecedor.Where(c => c.Deletado != true).ToList();
            var criticidades = contexto.Criticidade.Where(c => c.Deletado != true).ToList();

            ViewBag.material = new SelectList(materials, "id_material", "material");
            ViewBag.status = new SelectList(status, "id_status", "status");
            ViewBag.tipo = new SelectList(tipos, "id_tipo", "tipo");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("manutencao,id_status, descricao, data, data_prevista,  data_conclusao, id_tipo, id_material, id_setor, id_fornecedor, id_criticidade")] Manutencao createRequest, IFormFile anexoFile)
        {
            var temPermissao = User.HasClaim("PermissaoManutencao", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var manutencao = new Manutencao
            {
                manutencao = createRequest.manutencao,
                id_status = createRequest.id_status,
                descricao = createRequest.descricao,
                data = createRequest.data,
                data_prevista = createRequest.data_prevista,
                data_conclusao = createRequest.data_conclusao,
                id_tipo = createRequest.id_tipo,
                id_material = createRequest.id_material,
                id_setor = createRequest.id_setor,
                id_fornecedor = createRequest.id_fornecedor,
                id_criticidade = createRequest.id_criticidade
            };

            if (anexoFile != null && anexoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    manutencao.anexo = memoryStream.ToArray();
                }
            }

            contexto.Manutencao.Add(manutencao);
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
            var temPermissao = User.HasClaim("PermissaoManutencao", "True");
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

            var manutencaos = await contexto.Manutencao
                                            .Include(m => m.material)
                                            .Include(m => m.status)
                                            .Include(m => m.tipo)
                                            .Include(m => m.setor)
                                            .Include(m => m.fornecedor)
                                            .Include(m => m.criticidade)
                                            .FirstOrDefaultAsync(x => x.id_manutencao == id);
            if (manutencaos == null)
            {
                return NotFound();
            }

            return View(manutencaos);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var temPermissao = User.HasClaim("PermissaoManutencao", "True");
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

            var manutencao = await contexto.Manutencao
                .Include(x => x.status)
                .Include(x => x.tipo)
                .Include(x => x.material)
                .Include(x => x.setor)
                .Include(x => x.fornecedor)
                .Include(x => x.criticidade)
                .FirstOrDefaultAsync(x => x.id_manutencao == id);

            if (manutencao == null)
            {
                return NotFound();
            }
            var materials = await contexto.Material.Where(c => c.Deletado != true).ToListAsync();
            ViewBag.material = new SelectList(materials, "id_material", "material");
            
            var status = await contexto.Status.Where(c => c.Deletado != true).ToListAsync();
            ViewBag.status = new SelectList(status, "id_status", "status");

            var tipos = await contexto.Tipo.Where(c => c.Deletado != true).ToListAsync();
            ViewBag.tipo = new SelectList(tipos, "id_tipo", "tipo");

            var setors = await contexto.Setor.Where(c => c.Deletado != true).ToListAsync();
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");

            var fornecedors = await contexto.Fornecedor.Where(c => c.Deletado != true).ToListAsync();
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");

            var criticidades = await contexto.Criticidade.Where(c => c.Deletado != true).ToListAsync();
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");

            return View(manutencao);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Manutencao updateRequest, IFormFile anexoFile)
        {
            var manutencao = await contexto.Manutencao.FindAsync(updateRequest.id_manutencao);
            if (manutencao == null)
            {
                return NotFound();
            }

            manutencao.manutencao = updateRequest.manutencao;
            manutencao.id_status = updateRequest.id_status;
            manutencao.descricao = updateRequest.descricao;
            manutencao.id_tipo = updateRequest.id_tipo;
            manutencao.id_material = updateRequest.id_material;
            manutencao.id_setor = updateRequest.id_setor;
            manutencao.id_fornecedor = updateRequest.id_fornecedor;
            manutencao.id_criticidade = updateRequest.id_criticidade;
            if (anexoFile != null)
            {
                manutencao.anexo = null;

                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    manutencao.anexo = memoryStream.ToArray();
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
        public async Task<IActionResult> Delete(Manutencao deleteRequest)
        {
            var temPermissao = User.HasClaim("PermissaoManutencao", "True");
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

            var manutencao = await contexto.Manutencao.FindAsync(deleteRequest.id_manutencao);

            if (manutencao == null)
            {
                return NotFound();
            }

            manutencao.Deletado = true;
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
            var manutencao = await contexto.Manutencao.FindAsync(id);
            if (manutencao == null)
            {
                return NotFound();
            }

            if (manutencao.anexo == null || manutencao.anexo.Length == 0)
            {
                return NotFound("O anexo não está disponível.");
            }

            // Define o tipo MIME do arquivo
            string contentType = "application/octet-stream";

            // Define o nome do arquivo para download (opcional)
            string fileName = "anexo.pdf";

            // Retorna o arquivo como um resultado para download
            return File(manutencao.anexo, contentType, fileName);
        }


    }
}
