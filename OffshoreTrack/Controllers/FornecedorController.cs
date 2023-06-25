using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.AspNetCore.Authorization;

namespace OffshoreTrack.Controllers
{
    [Authorize]
    public class FornecedorController : Controller
    {
        private readonly Contexto contexto;

        public FornecedorController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fornecedores = await contexto.Fornecedor.ToListAsync();
            return View(fornecedores);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("fornecedor,razaoSocial, cnpj, endereco, telefone, email, vendedor")] Fornecedor createRequest)
        {
            var fornecedor = new Fornecedor
            {
                fornecedor = createRequest.fornecedor,
                razaoSocial = createRequest.razaoSocial,
                cnpj = createRequest.cnpj,
                endereco = createRequest.endereco,
                telefone = createRequest.telefone,
                email = createRequest.email,
                vendedor = createRequest.vendedor
            };

            contexto.Fornecedor.Add(fornecedor);
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
            var fornecedor = await contexto.Fornecedor.FirstOrDefaultAsync(x => x.id_fornecedor == id);
            return View(fornecedor);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var fornecedor = await contexto.Fornecedor.FirstOrDefaultAsync(x => x.id_fornecedor == id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Fornecedor updateRequest)
        {
            var fornecedor = await contexto.Fornecedor.FindAsync(updateRequest.id_fornecedor);
            if (fornecedor == null)
            {
                return NotFound();
            }

            fornecedor.fornecedor = updateRequest.fornecedor;
            fornecedor.razaoSocial = updateRequest.razaoSocial;
            fornecedor.cnpj = updateRequest.cnpj;
            fornecedor.endereco = updateRequest.endereco;
            fornecedor.telefone = updateRequest.telefone;
            fornecedor.email = updateRequest.email;
            fornecedor.vendedor = updateRequest.vendedor;

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
        public async Task<IActionResult> Delete(Fornecedor deleteRequest)
        {
            var fornecedor = await contexto.Fornecedor.FindAsync(deleteRequest.id_fornecedor);

            if (fornecedor == null)
            {
                return NotFound();
            }
            contexto.Fornecedor.Remove(fornecedor);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */
    }
}

