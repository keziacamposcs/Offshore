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
                                            .Include(m => m.material) 
                                            .Include(m => m.setor) 
                                            .Include(m => m.status)
                                            .ToListAsync();
            return View(manutencaos);
        }


        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            var materials = contexto.Material.ToList();
            var status = contexto.Status.ToList();
            var tipos = contexto.Tipo.ToList();

            var setors = contexto.Setor.ToList();
            var fornecedors = contexto.Fornecedor.ToList();
            var criticidades = contexto.Criticidade.ToList();

            ViewBag.material = new SelectList(materials, "id_material", "material");
            ViewBag.status = new SelectList(status, "id_status", "status");
            ViewBag.tipo = new SelectList(tipos, "id_tipo", "tipo");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("manutencao,id_status, descricao, data, data_prevista,  data_conclusao, anexo, id_tipo, id_material, id_setor, id_fornecedor, id_criticidade")] Manutencao createRequest)
        {
            var manutencaos = new Manutencao
            {
                manutencao = createRequest.manutencao,
                id_status = createRequest.id_status,
                descricao = createRequest.descricao,
                data = createRequest.data,
                data_prevista = createRequest.data_prevista,
                data_conclusao = createRequest.data_conclusao,
                anexo = createRequest.anexo,
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
            var manutencaos = await contexto.Manutencao
                                            .Include(m => m.material)
                                            .Include(m => m.status)
                                            .Include(m => m.material) 
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
            var materials = await contexto.Material.ToListAsync();
            ViewBag.material = new SelectList(materials, "id_material", "material");
            
            var status = await contexto.Status.ToListAsync();
            ViewBag.status = new SelectList(status, "id_status", "status");

            var tipos = await contexto.Tipo.ToListAsync();
            ViewBag.tipo = new SelectList(tipos, "id_tipo", "tipo");

            var setors = await contexto.Setor.ToListAsync();
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");

            var fornecedors = await contexto.Fornecedor.ToListAsync();
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");

            var criticidades = await contexto.Criticidade.ToListAsync();
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");

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
            manutencao.id_status = updateRequest.id_status;
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
