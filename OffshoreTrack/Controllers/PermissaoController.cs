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
        public async Task<IActionResult> Create(Permissao permissao)
        {
            if (ModelState.IsValid)
            {
                contexto.Add(permissao);
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permissao);
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
        public async Task<IActionResult> Delete(int id) // Alterado de DeleteConfirmed para Delete
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

        // Verificacao de Permissão
        private bool UserHasPermission(string permissionName)
        {
            // Obtenha o usuário atualmente autenticado
            var currentUser = contexto.Usuario
                .Include(u => u.Permissao)
                .FirstOrDefault(u => u.usuario == User.Identity.Name);

            // Verifique se o usuário e suas permissões existem
            if (currentUser != null && currentUser.Permissao != null)
            {
                var permissao = currentUser.Permissao;

                // Verifique a permissão específica com base no nome fornecido
                switch (permissionName)
                {
                    case "pode_criar":
                        return permissao.pode_criar;
                    case "pode_ler":
                        return permissao.pode_ler;
                    case "pode_atualizar":
                        return permissao.pode_atualizar;
                    case "pode_deletar":
                        return permissao.pode_deletar;
                    case "permissao_admin":
                        return permissao.permissao_admin;
                }
            }

            return false;
        }

        // Fim - Verificacao de Permissão

    }
}
