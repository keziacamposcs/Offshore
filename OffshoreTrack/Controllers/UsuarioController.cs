using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;
using OffshoreTrack.Models;
using Microsoft.AspNetCore.Authorization;

namespace OffshoreTrack.Controllers
{
    [Authorize]
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
            var usuarios = await contexto.Usuario.Include(l => l.Permissao).ToListAsync();
            return View(usuarios);
        }

        /* CRUD */

        // Create
        [HttpGet]
        public IActionResult New()
        {
            var permissoes = contexto.Permissao.ToList();
            if (!permissoes.Any())
            {
                throw new Exception("Não existem permissões na base de dados!");
            }

            ViewBag.PermissaoList = new SelectList(permissoes, "id_permissao", "nome_permissao");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario createRequest, string senha2)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Todos os campos devem ser preenchidos.");
                return View(createRequest);
            }

            var existingUser = await contexto.Usuario.FirstOrDefaultAsync(u => u.usuario == createRequest.usuario);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Este nome de usuário já está em uso.");
                return View(createRequest);
            }

            if (createRequest.senha != senha2)
            {
                ModelState.AddModelError(string.Empty, "As senhas não coincidem.");
                return View(createRequest);
            }

            var usuario = new Usuario
            {
                usuario = createRequest.usuario,
                senha = HashPassword(createRequest.senha),
                nome = createRequest.nome,
                cpf = createRequest.cpf,
                email = createRequest.email,
                id_permissao = createRequest.id_permissao
            };

            contexto.Usuario.Add(usuario);
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

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        // Fim - Create

        // Read
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var usuario = await contexto.Usuario.Include(x => x.Permissao).FirstOrDefaultAsync(x => x.id_usuario == id);
            return View(usuario);
        }
        // Fim - Read

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await contexto.Usuario.Include(u => u.Permissao).FirstOrDefaultAsync(x => x.id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            var permissoes = await contexto.Permissao.ToListAsync();
            if (!permissoes.Any())
            {
                throw new Exception("Não existem permissões na base de dados!");
            }

            ViewBag.PermissaoList = new SelectList(permissoes, "id_permissao", "nome_permissao", usuario.id_permissao);

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Usuario updateRequest)
        {
            if (!ModelState.IsValid)
            {
                var permissoes = await contexto.Permissao.ToListAsync();
                if (!permissoes.Any())
                {
                    throw new Exception("Não existem permissões na base de dados!");
                }

                ViewBag.PermissaoList = new SelectList(permissoes, "id_permissao", "nome_permissao", updateRequest.id_permissao);

                return View(updateRequest);
            }

            var usuario = await contexto.Usuario.FindAsync(updateRequest.id_usuario);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.usuario = updateRequest.usuario;
            usuario.nome = updateRequest.nome;
            usuario.cpf = updateRequest.cpf;
            usuario.email = updateRequest.email;
            usuario.id_permissao = updateRequest.id_permissao;

            if (!string.IsNullOrEmpty(updateRequest.senha))
            {
                usuario.senha = HashPassword(updateRequest.senha);
            }

            try
            {
                contexto.Usuario.Update(usuario); // Atualizar o objeto usuario no contexto
                await contexto.SaveChangesAsync(); // Salvar as alterações no banco de dados
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
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await contexto.Usuario.FindAsync(id);

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
