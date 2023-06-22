using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;

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
            var materials = await contexto.Material.ToListAsync();
            return View(materials);
        }
        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Material createRequest)
        {
            string qrCodeString = $"{createRequest.material};{createRequest.descricao};{createRequest.tamanho};{createRequest.tipo};{createRequest.criticidade};{createRequest.setor};{createRequest.usuario};{createRequest.fornecedor}";

            // Gera o QRCoder
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeString, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            // Obtém os bytes da imagem PNG do QRCode
            byte[] qrCodeImageBytes = qrCode.GetGraphic(20);

            // Converte a imagem PNG em base64
            string qrCodeAsBase64 = Convert.ToBase64String(qrCodeImageBytes);

            createRequest.cod_barra = qrCodeAsBase64;

            contexto.Material.Add(createRequest);
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
            var material = await contexto.Material.FirstOrDefaultAsync(x => x.id_material == id);
            return View(material);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var material = await contexto.Material.FirstOrDefaultAsync(x => x.id_material == id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Material updateRequest)
        {
            var material = await contexto.Material.FindAsync(updateRequest.id_material);
            if (material == null)
            {
                return NotFound();
            }

            material.material = updateRequest.material;
            material.descricao = updateRequest.descricao;
            material.tamanho = updateRequest.tamanho;
            material.cod_barra = updateRequest.cod_barra;
            material.anexo = updateRequest.anexo;
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
        // Fim - Delete

        /* Fim - CRUD */
    }
}

