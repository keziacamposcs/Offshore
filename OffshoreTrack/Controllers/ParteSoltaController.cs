using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting; 

namespace OffshoreTrack.Controllers
{
    public class ParteSoltaController : Controller
    {
        private readonly Contexto contexto;
        private readonly IWebHostEnvironment _hostingEnvironment; 

        [ActivatorUtilitiesConstructor]
        public ParteSoltaController(Contexto contexto, IWebHostEnvironment hostingEnvironment)
        {
            this.contexto = contexto;
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var parteSolta = await contexto.ParteSolta.ToListAsync();
            return View(parteSolta);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("partesolta,quantidade, anexo, id_oc, id_material")] ParteSolta createRequest)
        {
            var parteSolta = new ParteSolta
            {
                partesolta = createRequest.partesolta,
                quantidade = createRequest.quantidade,
                anexo = createRequest.anexo,
                id_oc = createRequest.id_oc,
                id_material = createRequest.id_material
            };

            contexto.ParteSolta.Add(parteSolta);
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
            var parteSolta = await contexto.ParteSolta.FirstOrDefaultAsync(x => x.id_partesolta == id);
            return View(parteSolta);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var parteSolta = await contexto.ParteSolta.FirstOrDefaultAsync(x => x.id_partesolta == id);
            if (parteSolta == null)
            {
                return NotFound();
            }
            return View(parteSolta);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ParteSolta updateRequest, IFormFile anexo)
        {
            var parteSolta = await contexto.ParteSolta.FindAsync(updateRequest.id_partesolta);
            if (parteSolta == null)
            {
                return NotFound();
            }

            // Trate o arquivo aqui
            // Por exemplo, você pode salvar o arquivo em um sistema de arquivos e armazenar o caminho do arquivo no banco de dados
            if (anexo != null)
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", anexo.FileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await anexo.CopyToAsync(stream);
                }

                parteSolta.anexo = filePath; // atualize a referência do arquivo em seu banco de dados
            }

            parteSolta.partesolta = updateRequest.partesolta;
            parteSolta.quantidade = updateRequest.quantidade;
            parteSolta.id_oc = updateRequest.id_oc;
            parteSolta.id_material = updateRequest.id_material;
            
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
        public async Task<IActionResult> Delete(ParteSolta deleteRequest)
        {
            var parteSolta = await contexto.ParteSolta.FindAsync(deleteRequest.id_partesolta);

            if (parteSolta == null)
            {
                return NotFound();
            }
            contexto.ParteSolta.Remove(parteSolta);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */



        /* Download */
        public async Task<IActionResult> DownloadAsync(int id)
        {
            var parteSolta = await contexto.ParteSolta.FindAsync(id);
            if (parteSolta == null)
            {
                return NotFound();
            }
            
            var path = parteSolta.anexo;
            var stream = System.IO.File.OpenRead(path);
            var fileDownloadName = Path.GetFileName(path);
            var mimeType = GetMimeType(fileDownloadName);
            
            return File(stream, mimeType, fileDownloadName);
        }

        // Esta função é usada para obter o tipo MIME de um arquivo com base em sua extensão
        public string GetMimeType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats officedocument.spreadsheetml.sheet";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "application/octet-stream";
            }
        }
        /* Fim - Download */
    }
}

