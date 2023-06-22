using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using QRCoder;
using Microsoft.AspNetCore.Http;


namespace OffshoreTrack.Controllers
{
    public class MaterialController : Controller
    {
        private readonly Contexto contexto;

        public MaterialController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var materials = await contexto.Material
                .Include(x => x.tipo)
                .Include(x => x.setor)
                .Include(x => x.local)
                .ToListAsync();

            return View(materials);
        }

        /* CRUD */

        // Create
        [HttpGet]
        public IActionResult New()
        {
            var tipos = contexto.Tipo.ToList();
            var criticidades = contexto.Criticidade.ToList();
            var setors = contexto.Setor.ToList();
            var clientes = contexto.Cliente.ToList();
            var locals = contexto.Local.ToList();
            var usuarios = contexto.Usuario.ToList();
            var fornecedors = contexto.Fornecedor.ToList();

            ViewBag.tipo = new SelectList(tipos, "id_tipo", "tipo");
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");
            ViewBag.local = new SelectList(locals, "id_local", "local");
            ViewBag.usuario = new SelectList(usuarios, "id_usuario", "usuario");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("material,descricao, tamanho, anexo, id_tipo, id_criticidade, id_setor, id_cliente, id_local, id_usuario, id_fornecedor")] Material createRequest)
        {
            var materials = new Material
            {
                material = createRequest.material,
                descricao = createRequest.descricao,
                numeroSerie = createRequest.numeroSerie,
                tamanho = createRequest.tamanho,
                anexo = createRequest.anexo,
                id_tipo = createRequest.id_tipo,
                id_criticidade = createRequest.id_criticidade,
                id_setor = createRequest.id_setor,
                id_cliente = createRequest.id_cliente,
                id_local = createRequest.id_local,
                id_usuario = createRequest.id_usuario,
                id_fornecedor = createRequest.id_fornecedor
            };

            contexto.Material.Add(materials);
            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(createRequest);
            }
        }

        // Read
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var material = await contexto.Material
                .Include(x => x.tipo)
                .Include(x => x.criticidade)
                .Include(x => x.setor)
                .Include(x => x.cliente)
                .Include(x => x.local)
                .Include(x => x.usuario)
                .Include(x => x.fornecedor)
                .Include(x => x.manutencaos)
                .FirstOrDefaultAsync(x => x.id_material == id);

            if (material != null)
            {
                return View(material);
            }
            else
            {
                return NotFound();
            }
        }

        // Gerador de Qrcode
        public async Task<IActionResult> GenerateQrCode(int id)
        {
            var material = await contexto.Material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            // Limpar o QR Code existente
            material.qrcode = null;


            // Gerar o QRCode para o material
            string qrCodeContent = $"<html>" +
                $"<head><title>Material: {material.id_material}</title></head>" +
                $"<body>" +
                    $"<h1>Material: {material.material}</h1>" +
                    $"<p>Descrição: {material.descricao}</p>" +
                    $"<p>Número de Série: {material.numeroSerie}</p>" +
                    $"<p>Tamanho: {material.tamanho}</p>" +
                    $"<p>Tipo: {material.tipo.tipo}</p>" +
                    $"<p>Criticidade: {material.criticidade.criticidade}</p>" +
                    $"<p>Setor: {material.setor.setor}</p>" +
                    $"<p>Cliente: {material.cliente.cliente}</p>" +
                    $"<p>Local: {material.local.local}</p>" +
                    $"<p>Usuário: {material.usuario.usuario}</p>" +
                    $"<p>Fornecedor: {material.fornecedor.fornecedor}</p>" +
                    (material.manutencaos != null && material.manutencaos.Count > 0
                        ? $"<h2>Últimas Manutenções:</h2><ul>" +
                            string.Join("", material.manutencaos.OrderByDescending(m => m.data).Take(5).Select(m => $"<li>{m.data}: {m.manutencao}</li>")) +
                        "</ul>"
                        : "") +
                "</body></html>";
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(qrCodeContent, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeAsBytes = qrCode.GetGraphic(20);
            var qrCodeAsBase64 = Convert.ToBase64String(qrCodeAsBytes);
            material.qrcode = qrCodeAsBase64;

            contexto.Material.Update(material);
            await contexto.SaveChangesAsync();

            return Content(qrCodeAsBase64);
        }

        // Anexo
        [HttpGet]
        public IActionResult DownloadAttachment(int id)
        {
            var material = contexto.Material.Find(id);
            if (material != null)
            {
                if (material.anexo != null)
                {
                    var contentType = "application/pdf";
                    return File(material.anexo, contentType, "anexo.pdf");
                }
            }
            return NotFound();
        }

        // Fim - Anexo

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var material = await contexto.Material.FirstOrDefaultAsync(x => x.id_material == id);
            var tipos = contexto.Tipo.ToList();
            var criticidades = contexto.Criticidade.ToList();
            var setors = contexto.Setor.ToList();
            var clientes = contexto.Cliente.ToList();
            var locals = contexto.Local.ToList();
            var usuarios = contexto.Usuario.ToList();
            var fornecedors = contexto.Fornecedor.ToList();

            ViewBag.tipo = new SelectList(tipos, "id_tipo", "tipo");
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");
            ViewBag.local = new SelectList(locals, "id_local", "local");
            ViewBag.usuario = new SelectList(usuarios, "id_usuario", "usuario");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");

         
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Material updateRequest, IFormFile newFile)
        {
            var material = await contexto.Material.FindAsync(updateRequest.id_material);
            if (material == null)
            {
                return NotFound();
            }

            material.material = updateRequest.material;
            material.descricao = updateRequest.descricao;
            material.numeroSerie = updateRequest.numeroSerie;
            material.tamanho = updateRequest.tamanho;
            material.qrcode = updateRequest.qrcode;

            if (newFile != null && newFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await newFile.CopyToAsync(memoryStream);
                material.anexo = memoryStream.ToArray();
            }

            material.id_tipo = updateRequest.id_tipo;
            material.id_criticidade = updateRequest.id_criticidade;
            material.id_setor = updateRequest.id_setor;
            material.id_cliente = updateRequest.id_cliente;
            material.id_local = updateRequest.id_local;
            material.id_usuario = updateRequest.id_usuario;
            material.id_fornecedor = updateRequest.id_fornecedor;
            
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
        private async Task<byte[]> ReadFileData(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        // Fim - Update

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Material deleteRequest)
        {
            var material = await contexto.Material.FindAsync(deleteRequest.id_material);

            if (material == null)
            {
                return NotFound();
            }
            contexto.Material.Remove(material);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /* Fim - CRUD */
    }
}
