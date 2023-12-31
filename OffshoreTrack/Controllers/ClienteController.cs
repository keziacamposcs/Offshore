﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ClienteController : Controller
    {
        private readonly Contexto contexto;

        public ClienteController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var temPermissao = User.HasClaim("PermissaoCliente", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var clientes = await contexto.Cliente.Where(c => c.Deletado != true).ToListAsync();
            return View(clientes);
        }

        /* CRUD */

        // Create
        [HttpGet]
        public IActionResult New()
        {
            var temPermissao = User.HasClaim("PermissaoCliente", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeCriar = User.HasClaim("PodeCriar", "True");
            if(!podeCriar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }


            var clientes = contexto.Cliente.ToList();

            if (clientes == null || !clientes.Any())
            {
                Console.WriteLine("Não foi possível obter clientes do banco de dados");
            }

            ViewBag.cliente = new SelectList(clientes, "id_cliente", "cliente");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("cliente, razaoSocial, cnpj, endereco, telefone")] Cliente createRequest)
        {
            var cliente = new Cliente
            {
                cliente = createRequest.cliente,
                razaoSocial = createRequest.razaoSocial,
                cnpj = createRequest.cnpj,
                endereco = createRequest.endereco,
                telefone = createRequest.telefone
            };

            contexto.Cliente.Add(cliente);
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
            var temPermissao = User.HasClaim("PermissaoCliente", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeLer = User.HasClaim("PodeLer", "True");
            if(!podeLer)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var cliente = await contexto.Cliente.FirstOrDefaultAsync(x => x.id_cliente == id);
            return View(cliente);
        }
        // Fim - Read


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var temPermissao = User.HasClaim("PermissaoCliente", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeAtualizar = User.HasClaim("PodeAtualizar", "True");
            if(!podeAtualizar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var cliente = await contexto.Cliente.FirstOrDefaultAsync(x => x.id_cliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Cliente updateRequest)
        {
            var cliente = await contexto.Cliente.FindAsync(updateRequest.id_cliente);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.cliente = updateRequest.cliente;
            cliente.razaoSocial = updateRequest.razaoSocial;
            cliente.cnpj = updateRequest.cnpj;
            cliente.endereco = updateRequest.endereco;
            cliente.telefone = updateRequest.telefone;

            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Fim - Update


        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete (Cliente deleteRequest)
        {
            var temPermissao = User.HasClaim("PermissaoCliente", "True");
            if(!temPermissao)
            {    
                TempData["Aviso"] = "Você não tem permissão para acessar esta página. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }
            var podeDeletar = User.HasClaim("PodeDeletar", "True");
            if(!podeDeletar)
            {    
                TempData["Aviso"] = "Você não tem permissão para realizar essa operação. Entre em contato com o administrador do sistema.";
                return RedirectToAction("Index", "Home");
            }

            var cliente = await contexto.Cliente.FindAsync(deleteRequest.id_cliente);

            if (cliente == null)
            {
                return NotFound();
            }
            cliente.Deletado = true;
            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Fim - Delete

        /* Fim - CRUD */
    }
}

