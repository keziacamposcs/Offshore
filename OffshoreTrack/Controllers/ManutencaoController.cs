using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;

namespace OffshoreTrack.Controllers
{
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
            var manutencaos = await contexto.Manutencao
                                            .Include(m => m.material) // inclui os dados de Material
                                            .Include(m => m.setor)    // inclui os dados de Setor
                                            .Include(m => m.fornecedor)  // inclui os dados de Fornecedor
                                            .Include(m => m.criticidade)  // inclui os dados de Criticidade
                                            .ToListAsync();
            return View(manutencaos);
        }


        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var materials = contexto.Material.ToList();
            var setors = contexto.Setor.ToList();
            var fornecedors = contexto.Fornecedor.ToList();
            var criticidades = contexto.Criticidade.ToList();

            ViewBag.material = new SelectList(materials, "id_material", "material");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("manutencao,descricao, id_tipo, id_material, id_setor, id_fornecedor, id_criticidade")] Manutencao createRequest)
        {
            var manutencaos = new Manutencao
            {
                manutencao = createRequest.manutencao,
                descricao = createRequest.descricao,
                id_tipo = createRequest.id_tipo,
                id_material = createRequest.id_material,
                id_setor = createRequest.id_setor,
                id_fornecedor = createRequest.id_fornecedor,
                id_criticidade = createRequest.id_criticidade
            };

            contexto.Manutencao.Add(manutencaos);
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
            var manutencao = await contexto.Manutencao.FirstOrDefaultAsync(x => x.id_manutencao == id);
            return View(manutencao);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var manutencao = await contexto.Manutencao.FirstOrDefaultAsync(x => x.id_manutencao == id);
            if (manutencao == null)
            {
                return NotFound();
            }
            return View(manutencao);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Manutencao updateRequest)
        {
            var manutencao = await contexto.Manutencao.FindAsync(updateRequest.id_manutencao);
            if (manutencao == null)
            {
                return NotFound();
            }

            manutencao.manutencao = updateRequest.manutencao;
            manutencao.descricao = updateRequest.descricao;
            manutencao.id_tipo = updateRequest.id_tipo;
            manutencao.id_material = updateRequest.id_material;
            manutencao.id_setor = updateRequest.id_setor;
            manutencao.id_fornecedor = updateRequest.id_fornecedor;
            manutencao.id_criticidade = updateRequest.id_criticidade;
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
            var manutencao = await contexto.Manutencao.FindAsync(deleteRequest.id_manutencao);

            if (manutencao == null)
            {
                return NotFound();
            }
            contexto.Manutencao.Remove(manutencao);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */
    }
}

