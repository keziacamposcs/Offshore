using System;
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
    public class PermissaoController : Controller
    {
        private readonly Contexto contexto;

        public PermissaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var permissao = await contexto.Permissao.ToListAsync();
            return View(permissao);
        }

        /* CRUD */
        // Create
// Create
        [HttpGet]
        public IActionResult New()
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("nome_permissao, pode_criar, pode_ler, pode_atualizar, pode_deletar, permissao_admin, permissaoCertificado, permissaoCliente, permissaoContrato, permissaoCriticidade, permissaoFornecedor, permissaoLocal, permissaoManutencao, permissaoMaterial, permissaoOrdemCompra, permissaoParteSolta, permissaoRateio, permissaoSetor, permissaoTipo")] Permissao createRequest)
        {
            var permissoes = new Permissao
            {
                nome_permissao = createRequest.nome_permissao,
                pode_criar = createRequest.pode_criar,
                pode_ler = createRequest.pode_ler,
                pode_atualizar = createRequest.pode_atualizar,
                pode_deletar = createRequest.pode_deletar,
                permissao_admin = createRequest.permissao_admin,
                permissaoCertificado = createRequest.permissaoCertificado,
                permissaoCliente = createRequest.permissaoCliente,
                permissaoContrato = createRequest.permissaoContrato,
                permissaoCriticidade = createRequest.permissaoCriticidade,
                permissaoFornecedor = createRequest.permissaoFornecedor,
                permissaoLocal = createRequest.permissaoLocal,
                permissaoManutencao = createRequest.permissaoManutencao,
                permissaoMaterial = createRequest.permissaoMaterial,
                permissaoOrdemCompra = createRequest.permissaoOrdemCompra,
                permissaoParteSolta = createRequest.permissaoParteSolta,
                permissaoRateio = createRequest.permissaoRateio,
                permissaoSetor = createRequest.permissaoSetor,
                permissaoTipo = createRequest.permissaoTipo
            };

            contexto.Permissao.Add(permissoes);
            
            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction("Read", new { id = permissoes.id_permissao });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Fim - Create

        // Read
        [HttpGet]
        public async Task<IActionResult> Read(int? id)
        {

            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var permissao = await contexto.Permissao
                .FirstOrDefaultAsync(m => m.id_permissao == id);
            if (permissao == null)
            {
                return NotFound();
            }

            return View(permissao);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var permissao = await contexto.Permissao.FindAsync(id);
            if (permissao == null)
            {
                return NotFound();
            }
            return View(permissao);
        }

       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Permissao permissao)
        {
            if (ModelState.IsValid)
            {     
                permissao.pode_criar = Request.Form["pode_criar"].Contains("true");
                permissao.pode_ler = Request.Form["pode_ler"].Contains("true");
                permissao.pode_atualizar = Request.Form["pode_atualizar"].Contains("true");
                permissao.pode_deletar = Request.Form["pode_deletar"].Contains("true");
                permissao.permissao_admin = Request.Form["permissao_admin"].Contains("true");

                permissao.permissaoCertificado = Request.Form["permissaoCertificado"].Contains("true");
                permissao.permissaoCliente = Request.Form["permissaoCliente"].Contains("true");
                permissao.permissaoContrato = Request.Form["permissaoContrato"].Contains("true");
                permissao.permissaoCriticidade = Request.Form["permissaoCriticidade"].Contains("true");
                permissao.permissaoFornecedor = Request.Form["permissaoFornecedor"].Contains("true");
                permissao.permissaoLocal = Request.Form["permissaoLocal"].Contains("true");
                permissao.permissaoManutencao = Request.Form["permissaoManutencao"].Contains("true");
                permissao.permissaoMaterial = Request.Form["permissaoMaterial"].Contains("true");
                permissao.permissaoOrdemCompra = Request.Form["permissaoOrdemCompra"].Contains("true");
                permissao.permissaoParteSolta = Request.Form["permissaoParteSolta"].Contains("true");
                permissao.permissaoRateio = Request.Form["permissaoRateio"].Contains("true");
                permissao.permissaoSetor = Request.Form["permissaoSetor"].Contains("true");
                permissao.permissaoTipo = Request.Form["permissaoTipo"].Contains("true");


                try
                {
                    contexto.Update(permissao);
                    await contexto.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissaoExists(permissao.id_permissao))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(permissao);
        }
        // Fim - Update

        // Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {    
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var permissao = await contexto.Permissao.FindAsync(id);
            contexto.Permissao.Remove(permissao);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissaoExists(int id)
        {
            return contexto.Permissao.Any(e => e.id_permissao == id);
        }
        // Fim - Delete

        /* Fim - CRUD */
    }
}
