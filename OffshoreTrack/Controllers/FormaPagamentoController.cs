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
    public class FormaPagamentoController : Controller
    {

        private readonly Contexto contexto;

        public FormaPagamentoController (Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var formaPagamento = await contexto.FormaPagamento.ToListAsync();
            var descricao = await contexto.FormaPagamento.ToListAsync();
            return View(formaPagamento);
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
        public async Task<IActionResult> Create([Bind("formaPagamento, descricao")] FormaPagamento createRequest)
        {
            var formaPagamentos = new FormaPagamento
            {
                formaPagamento = createRequest.formaPagamento,
                descricao = createRequest.descricao
            };

            contexto.FormaPagamento.Add(formaPagamentos);
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
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var formaPagamento = await contexto.FormaPagamento.FirstOrDefaultAsync(x => x.id_formaPagamento == id);
            var descricao = await contexto.FormaPagamento.FirstOrDefaultAsync(x => x.id_formaPagamento == id);
            return View(formaPagamento);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var formaPagamento = await contexto.FormaPagamento.FirstOrDefaultAsync(x => x.id_formaPagamento == id);
            if (formaPagamento == null)
            {
                return NotFound();
            }
            return View(formaPagamento);
        }

       [HttpPost]
        public async Task<IActionResult> Update(FormaPagamento updateRequest)
        {
            var formaPagamentos = await contexto.FormaPagamento.FindAsync(updateRequest.id_formaPagamento);
            if (formaPagamentos == null)
            {
                return NotFound();
            }

            formaPagamentos.formaPagamento = updateRequest.formaPagamento;
            formaPagamentos.descricao = updateRequest.descricao;
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
        public async Task<IActionResult> Delete(FormaPagamento deleteRequest)
        {
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin)
            {
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var formaPagamentos = await contexto.FormaPagamento.FindAsync(deleteRequest.id_formaPagamento);

            if (formaPagamentos == null)
            {
                return NotFound();
            }
            contexto.FormaPagamento.Remove(formaPagamentos);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */

    }
}