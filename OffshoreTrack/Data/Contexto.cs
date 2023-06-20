using System;
using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Models;

namespace OffshoreTrack.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        
        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Criticidade> Criticidade { get; set; }

        public DbSet<Fornecedor> Fornecedor { get; set; }

        public DbSet<Local> Local { get; set; }

        public DbSet<Manutencao> Manutencao { get; set; }

        public DbSet<Material> Material { get; set; }

        public DbSet<OrdemCompra> OrdemCompra { get; set; }

        public DbSet<ParteSolta> ParteSolta { get; set; }

        public DbSet<Rateio> Rateio { get; set; }

        public DbSet<Setor> Setor { get; set; }

        public DbSet<Tipo> Tipo { get; set; }

        public DbSet<Usuario> Usuario { get; set; }


        //Relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cliente
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.materials)
                .WithOne(m => m.cliente)
                .HasForeignKey(m => m.id_cliente);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.locals)
                .WithOne(l => l.Cliente)
                .HasForeignKey(l => l.id_cliente);

            // Criticidade
            modelBuilder.Entity<Criticidade>()
                .HasMany(c => c.manutencaos)
                .WithOne(m => m.criticidade)
                .HasForeignKey(m => m.id_criticidade);

            modelBuilder.Entity<Criticidade>()
                .HasMany(c => c.materials)
                .WithOne(m => m.criticidade)
                .HasForeignKey(m => m.id_criticidade);

            // Fornecedor
            modelBuilder.Entity<Fornecedor>()
                .HasMany(f => f.manutencaos)
                .WithOne(m => m.fornecedor)
                .HasForeignKey(m => m.id_fornecedor);

            modelBuilder.Entity<Fornecedor>()
                .HasMany(f => f.materials)
                .WithOne(m => m.fornecedor)
                .HasForeignKey(m => m.id_fornecedor);

            // Local
            modelBuilder.Entity<Local>()
                .HasMany(l => l.materials)
                .WithOne(m => m.local)
                .HasForeignKey(m => m.id_local);

            //Manutencao
            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.material)
                .WithMany(ma => ma.manutencaos)
                .HasForeignKey(m => m.id_material);

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.setor)
                .WithMany(s => s.manutencaos)
                .HasForeignKey(m => m.id_setor);

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.fornecedor)
                .WithMany(f => f.manutencaos)
                .HasForeignKey(m => m.id_fornecedor);

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.criticidade)
                .WithMany(c => c.manutencaos)
                .HasForeignKey(m => m.id_criticidade);

            //Material
            modelBuilder.Entity<Material>()
                .HasOne(m => m.tipo)
                .WithMany(t => t.materials)
                .HasForeignKey(m => m.id_tipo);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.criticidade)
                .WithMany(c => c.materials)
                .HasForeignKey(m => m.id_criticidade);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.setor)
                .WithMany(s => s.materials)
                .HasForeignKey(m => m.id_setor);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.cliente)
                .WithMany(c => c.materials)
                .HasForeignKey(m => m.id_cliente);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.local)
                .WithMany(l => l.materials)
                .HasForeignKey(m => m.id_local);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.usuario)
                .WithMany(u => u.materials)
                .HasForeignKey(m => m.id_usuario);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.fornecedor)
                .WithMany(f => f.materials)
                .HasForeignKey(m => m.id_fornecedor);

            // Ordem Compra
            modelBuilder.Entity<OrdemCompra>()
                .HasMany(oc => oc.parteSoltas)
                .WithOne(ps => ps.oc)
                .HasForeignKey(ps => ps.id_oc);


            // Parte Solta
            modelBuilder.Entity<ParteSolta>()
                .HasOne(ps => ps.material)
                .WithMany(m => m.parteSoltas)
                .HasForeignKey(ps => ps.id_material);

            // Rateio
            modelBuilder.Entity<Rateio>()
                .HasOne(r => r.setor1)
                .WithMany(s => s.rateios1)
                .HasForeignKey(r => r.id_setor1);

            modelBuilder.Entity<Rateio>()
                .HasOne(r => r.setor2)
                .WithMany(s => s.rateios2)
                .HasForeignKey(r => r.id_setor2);

            /* Tabelas */
            modelBuilder.Entity<Cliente>()
            .ToTable("cliente");

            modelBuilder.Entity<Criticidade>()
            .ToTable("criticidade");

            modelBuilder.Entity<Fornecedor>()
            .ToTable("fornecedor");

            modelBuilder.Entity<Local>()
            .ToTable("local");

            modelBuilder.Entity<Manutencao>()
            .ToTable("manutencao");

            modelBuilder.Entity<Material>()
            .ToTable("material");

            modelBuilder.Entity<OrdemCompra>()
            .ToTable("ordemCompra");

            modelBuilder.Entity<ParteSolta>()
            .ToTable("parteSolta");

            modelBuilder.Entity<Rateio>()
            .ToTable("rateio");

            modelBuilder.Entity<Setor>()
            .ToTable("setor");

            modelBuilder.Entity<Tipo>()
            .ToTable("tipo");

            modelBuilder.Entity<Usuario>()
            .ToTable("usuario");

            /* Fim - Tabela */
        }

    }
}