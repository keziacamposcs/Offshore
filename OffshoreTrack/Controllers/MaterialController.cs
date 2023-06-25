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
            var status = contexto.Status.ToList();

            ViewBag.tipo = new SelectList(tipos, "id_tipo", "tipo");
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");
            ViewBag.local = new SelectList(locals, "id_local", "local");
            ViewBag.usuario = new SelectList(usuarios, "id_usuario", "usuario");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.status = new SelectList(status, "id_status", "status");

            return View();
        }

       [HttpPost]
        public async Task<IActionResult> Create([Bind("material,descricao,numeroSerie, dimensoes, peso, dataFabricacao, dataValidade  , anexo, id_tipo, id_criticidade, id_setor, id_cliente, id_local, id_usuario, id_fornecedor, id_status, id_certificacao")] Material createRequest,IFormFile anexoFile)
        {
            var materials = new Material
            {
                material = createRequest.material,
                descricao = createRequest.descricao,
                numeroSerie = createRequest.numeroSerie,
                dimensoes = createRequest.dimensoes,
                peso = createRequest.peso,
                dataFabricacao = createRequest.dataFabricacao,
                dataValidade = createRequest.dataValidade,
                anexo = createRequest.anexo,
                id_tipo = createRequest.id_tipo,
                id_criticidade = createRequest.id_criticidade,
                id_setor = createRequest.id_setor,
                id_cliente = createRequest.id_cliente,
                id_local = createRequest.id_local,
                id_usuario = createRequest.id_usuario,
                id_fornecedor = createRequest.id_fornecedor,
                id_status = createRequest.id_status,
                id_certificacao = createRequest.id_certificacao
            };

            if (anexoFile != null && anexoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    materials.anexo = memoryStream.ToArray();
                }
            }
            else
            {
                materials.anexo = null; 
            }

            contexto.Material.Add(materials);
            await contexto.SaveChangesAsync();

            materials.qrcode = await GenerateQrCode(materials.id_material);

            contexto.Material.Update(materials);
            await contexto.SaveChangesAsync();

            var atividadeLogCreate = new AtividadeLog
            {
                id_material = materials.id_material,
                Timestamp = DateTime.Now,
                Acao = "Material criado"
            };

            await LogAtividade(atividadeLogCreate);

            return RedirectToAction("Read", new { id = materials.id_material });
        }
        // Fim  - Create

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
                .Include(x => x.certificacao)
                .Include(x => x.status)
                .Include(x => x.atividadeLogs)
                .FirstOrDefaultAsync(x => x.id_material == id);

            if (material == null)
            {
                return NotFound();
            }
            else
            {
                return View(material);
            }
        }
        // Fim - Read

        // Gerador de Qrcode
        public async Task<string> GenerateQrCode(int id)
        {
            var material = await contexto.Material.FindAsync(id);
            var tipos = contexto.Tipo.ToList();
            var criticidades = contexto.Criticidade.ToList();
            var setors = contexto.Setor.ToList();
            var clientes = contexto.Cliente.ToList();
            var locals = contexto.Local.ToList();
            var usuarios = contexto.Usuario.ToList();
            var fornecedors = contexto.Fornecedor.ToList();
            var status = contexto.Status.ToList();
            var certificacaos = contexto.Certificacao.ToList();

            if (material == null)
            {
                return null; // ou lançar uma exceção, dependendo da sua lógica de negócios
            }

        string qrCodeContent = $"Material: {material.material}\n" +
                       $"Descrição: {material.descricao ?? "N/A"}\n" +
                       $"Número de Série: {material.numeroSerie ?? "N/A"}\n" +
                       $"Dimensões: {material.dimensoes ?? "N/A"}\n" +
                       $"Peso: {material.peso ?? "N/A"}\n" +
                       $"Tipo: {material?.tipo?.tipo ?? "N/A"}\n" +
                       $"Criticidade: {material?.criticidade?.criticidade ?? "N/A"}\n" +
                       $"Setor: {material?.setor?.setor ?? "N/A"}\n" +
                       $"Cliente: {material?.cliente?.cliente ?? "N/A"}\n" +
                       $"Local: {material?.local?.local ?? "N/A"}\n" +
                       $"Fornecedor: {material?.fornecedor?.fornecedor ?? "N/A"}";
            Console.WriteLine(qrCodeContent);

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(qrCodeContent, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeAsBytes = qrCode.GetGraphic(20);
            var qrCodeAsBase64 = Convert.ToBase64String(qrCodeAsBytes);

            return qrCodeAsBase64;
        }

        // Fim - Gerador de Qrcode

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
            var status = contexto.Status.ToList();
            var certificacaos = contexto.Certificacao.ToList();

            ViewBag.tipo = new SelectList(tipos, "id_tipo", "tipo");
            ViewBag.criticidade = new SelectList(criticidades, "id_criticidade", "criticidade");
            ViewBag.setor = new SelectList(setors, "id_setor", "setor");
            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");
            ViewBag.local = new SelectList(locals, "id_local", "local");
            ViewBag.usuario = new SelectList(usuarios, "id_usuario", "usuario");
            ViewBag.fornecedor = new SelectList(fornecedors, "id_fornecedor", "fornecedor");
            ViewBag.status = new SelectList(status, "id_status", "status");
            ViewBag.certificacao = new SelectList(certificacaos, "id_certificacao", "certificacao");

         
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Material updateRequest, IFormFile anexoFile)
        {
            var material = await contexto.Material.FindAsync(updateRequest.id_material);
            if (material == null)
            {
                return NotFound();
            }

            material.material = updateRequest.material;
            material.descricao = updateRequest.descricao;
            material.numeroSerie = updateRequest.numeroSerie;
            material.dimensoes = updateRequest.dimensoes;
            material.peso = updateRequest.peso;
            material.qrcode = updateRequest.qrcode;
            material.id_tipo = updateRequest.id_tipo;
            material.id_criticidade = updateRequest.id_criticidade;
            material.id_setor = updateRequest.id_setor;
            material.id_cliente = updateRequest.id_cliente;
            material.id_local = updateRequest.id_local;
            material.id_usuario = updateRequest.id_usuario;
            material.id_fornecedor = updateRequest.id_fornecedor;
            material.id_status = updateRequest.id_status;
            material.id_certificacao = updateRequest.id_certificacao;

            if (anexoFile != null)
            {
                material.anexo = null;
                using (var memoryStream = new MemoryStream())
                {
                    await anexoFile.CopyToAsync(memoryStream);
                    material.anexo = memoryStream.ToArray();
                }
            }
            contexto.Material.Update(material);
            await contexto.SaveChangesAsync();
            material.qrcode = await GenerateQrCode(material.id_material);
            contexto.Material.Update(material);
            await contexto.SaveChangesAsync();

            var atividadeLogCreate = new AtividadeLog
            {
                id_material = material.id_material,
                Timestamp = DateTime.Now,
                Acao = "Material Atualizado"
            };
            await LogAtividade(atividadeLogCreate);
            return RedirectToAction("Read", new { id = material.id_material });
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

        // Download de Qrcode
        [HttpGet]
        public async Task<IActionResult> DownloadQrCode(int id)
        {
            var material = await contexto.Material.FindAsync(id);
            if (material == null || material.qrcode == null)
            {
                return NotFound();
            }

            var qrCodeAsBytes = Convert.FromBase64String(material.qrcode);
            return File(qrCodeAsBytes, "image/png", $"qrcode_{id}.png");
        }
        // Fim - Download de Qrcode
    
        // Log de Atividade
        public async Task LogAtividade(AtividadeLog atividadeLog)
        {
            if (atividadeLog != null)
            {
                contexto.AtividadeLog.Add(atividadeLog);
                await contexto.SaveChangesAsync();
            }
        }
        // Fim - Log de Atividade

        // Download de Anexo
        [HttpGet]
        public async Task<IActionResult> DownloadAnexo(int id)
        {
            var materials = await contexto.Material.FindAsync(id);
            
            if (materials == null)
            {
                return NotFound();
            }

            if (materials.anexo == null || materials.anexo.Length == 0)
            {
                return NotFound("O anexo não está disponível.");
            }

            // Define o tipo MIME do arquivo
            string contentType = "application/octet-stream";

            // Define o nome do arquivo para download (opcional)
            string fileName = "anexo.pdf";

            // Retorna o arquivo como um resultado para download
            return File(materials.anexo, contentType, fileName);
        }
        // Fim - Download de Anexo
    }

    
}
