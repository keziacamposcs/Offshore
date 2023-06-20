using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.EntityFrameworkCore;


namespace OffshoreTrack.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Contexto contexto;

        public UsuarioController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuario = await contexto.Usuario.ToListAsync();
            return View(usuario);
        }

        /* CRUD */

        // Create

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("usuario, senha, nome, cpf")] Usuario createRequest)
        {
            var usuario = new Usuario
            {
                usuario = createRequest.usuario,
                senha = createRequest.senha,
                nome = createRequest.nome,
                cpf = createRequest.cpf

            };

            contexto.Usuario.Add(usuario);
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
            var usuario = await contexto.Usuario.FirstOrDefaultAsync(x => x.id_usuario == id);
            return View(usuario);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await contexto.Usuario.FirstOrDefaultAsync(x => x.id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Usuario updateRequest)
        {
            var usuario = await contexto.Usuario.FindAsync(updateRequest.id_usuario);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.usuario = updateRequest.usuario;
            usuario.senha = updateRequest.senha;
            usuario.nome = updateRequest.nome;
            usuario.cpf = updateRequest.cpf;
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
        public async Task<IActionResult> Delete(Usuario deleteRequest)
        {
            var usuario = await contexto.Usuario.FindAsync(deleteRequest.id_usuario);

            if (usuario == null)
            {
                return NotFound();
            }
            contexto.Usuario.Remove(usuario);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - Delete

        /* Fim - CRUD */

    }
}