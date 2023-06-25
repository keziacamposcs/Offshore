using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Models;
using System.Diagnostics;
using OffshoreTrack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace OffshoreTrack.Controllers
{
    [Authorize]
    public class EmpresaController : Controller
    {
        private readonly Contexto contexto;

        public EmpresaController(Contexto contexto)
        {
            this.contexto = contexto;
        }


        // Read
        [HttpGet]
        public IActionResult Read()
        {
            var empresa = contexto.Empresa.FirstOrDefault();

            if (empresa.logoEmpresa != null)
            {
                var logoEmpresaBase64 = Convert.ToBase64String(empresa.logoEmpresa);
                ViewBag.LogoEmpresa = $"data:image/jpeg;base64,{logoEmpresaBase64}";
            }

            return View(empresa);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public IActionResult Edit()
        {
            var empresa = contexto.Empresa.FirstOrDefault();
            if (empresa == null)
            {
                empresa = new Empresa();
            }

            return View(empresa);
        }

[HttpPost]
public async Task<IActionResult> Update(Empresa editRequest, IFormFile logoEmpresa)
{
    Empresa empresa;

    if (editRequest.id_empresa != 0)
    {
        // Entidade existente
        empresa = await contexto.Empresa.FindAsync(editRequest.id_empresa);
        if (empresa == null)
        {
            return NotFound();
        }
    }
    else
    {
        // Nova entidade
        empresa = new Empresa();
        contexto.Empresa.Add(empresa);
    }

    // Atualiza a entidade
    empresa.empresa = editRequest.empresa;
    empresa.razaoSocialEmpresa = editRequest.razaoSocialEmpresa;
    empresa.cnpjEmpresa = editRequest.cnpjEmpresa;
    empresa.inscricaoEstadualEmpresa = editRequest.inscricaoEstadualEmpresa;
    empresa.inscricaoMunicipalEmpresa = editRequest.inscricaoMunicipalEmpresa;
    empresa.enderecoEmpresa = editRequest.enderecoEmpresa;
    empresa.telefoneEmpresa = editRequest.telefoneEmpresa;
    empresa.emailEmpresa = editRequest.emailEmpresa;
    empresa.responsavelEmpresa = editRequest.responsavelEmpresa;

    // Atualizar a imagem da empresa, se uma nova imagem foi enviada
    if (logoEmpresa != null && logoEmpresa.Length > 0)
    {
        empresa.logoEmpresa = null;
        using (var memoryStream = new MemoryStream())
        {
            await logoEmpresa.CopyToAsync(memoryStream);
            empresa.logoEmpresa = memoryStream.ToArray();
        }
    }

    await contexto.SaveChangesAsync();
    return RedirectToAction("Index", "Home");
}

        // Fim - Update
    }
}
