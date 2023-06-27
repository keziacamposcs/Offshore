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
        public DbSet<Certificacao> Certificacao { get; set; }

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
        public DbSet<Permissao> Permissao { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<AtividadeLog> AtividadeLog { get; set; }

        public DbSet<AtividadeLogPS> AtividadeLogPS { get; set; }

        public DbSet<Empresa> Empresa { get; set; }

        public DbSet<FormaPagamento> FormaPagamento { get; set; }

        public DbSet<Contrato> Contrato { get; set; }
 
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

            
            // Certificacao


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

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.tipo)
                .WithMany(t => t.manutencaos)
                .HasForeignKey(m => m.id_tipo);

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.status)
                .WithMany(t => t.manutencaos)
                .HasForeignKey(m => m.id_status);


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

            modelBuilder.Entity<Material>()
                .HasOne(m => m.certificacao)
                .WithMany(f => f.materials)
                .HasForeignKey(m => m.id_certificacao);


            // Ordem Compra
            modelBuilder.Entity<OrdemCompra>()
                .HasOne(oc => oc.fornecedor)
                .WithMany(f => f.OrdensCompra1)
                .HasForeignKey(oc => oc.id_fornecedor);

            modelBuilder.Entity<OrdemCompra>()
                .HasOne(oc => oc.fornecedor2)
                .WithMany(f => f.OrdensCompra2) 
                .HasForeignKey(oc => oc.id_fornecedor2);

            modelBuilder.Entity<OrdemCompra>()
                .HasOne(oc => oc.fornecedor3)
                .WithMany(f => f.OrdensCompra3)  
                .HasForeignKey(oc => oc.id_fornecedor3);

            modelBuilder.Entity<OrdemCompra>()
                .HasOne(oc => oc.setor)
                .WithMany(s => s.ordemCompras)  
                .HasForeignKey(oc => oc.id_setor);

            modelBuilder.Entity<OrdemCompra>()
                .HasOne(oc => oc.status)
                .WithMany(s => s.ordemCompras)  
                .HasForeignKey(oc => oc.id_status);

            modelBuilder.Entity<OrdemCompra>()
                .HasOne(oc => oc.rateio)
                .WithMany(s => s.ordemCompras)  
                .HasForeignKey(oc => oc.id_rateio);

            modelBuilder.Entity<OrdemCompra>()
                .HasOne(oc => oc.usuario)
                .WithMany(s => s.ordemCompras)  
                .HasForeignKey(oc => oc.id_usuario);

            modelBuilder.Entity<OrdemCompra>()
                .HasOne(o => o.empresa)
                .WithMany(e => e.ordemCompras)
                .HasForeignKey(o => o.id_empresa);

            modelBuilder.Entity<OrdemCompra>()
                .HasOne(o => o.formaPagamento)
                .WithMany(e => e.ordemCompras)
                .HasForeignKey(o => o.id_formaPagamento);


            // Parte Solta
            modelBuilder.Entity<ParteSolta>()
                .HasOne(ps => ps.material)
                .WithMany(m => m.parteSoltas)
                .HasForeignKey(ps => ps.id_material);

            modelBuilder.Entity<ParteSolta>()
                .HasOne(ps => ps.certificacao)
                .WithMany(m => m.parteSoltas)
                .HasForeignKey(ps => ps.id_certificacao);


            // Rateio
            modelBuilder.Entity<Rateio>()
                .HasOne(r => r.setor1)
                .WithMany(s => s.rateios1)
                .HasForeignKey(r => r.id_setor1);

            modelBuilder.Entity<Rateio>()
                .HasOne(r => r.setor2)
                .WithMany(s => s.rateios2)
                .HasForeignKey(r => r.id_setor2);

            //Usuario
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Permissao)
                .WithMany(p => p.usuarios)
                .HasForeignKey(u => u.id_permissao);

            //Permissao
            modelBuilder.Entity<Permissao>()
                .HasMany(p => p.usuarios)
                .WithOne(u => u.Permissao)
                .HasForeignKey(u => u.id_permissao);

            //Contrato 
            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.cliente)
                .WithMany(c => c.contratos)
                .HasForeignKey(c => c.id_cliente);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.fornecedor)
                .WithMany(s => s.contratos)
                .HasForeignKey(c => c.id_fornecedor);
            
            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.status)
                .WithMany(s => s.contratos)
                .HasForeignKey(c => c.id_status);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.setor)
                .WithMany(s => s.contratos)
                .HasForeignKey(c => c.id_setor);


            // AtividadeLog
            modelBuilder.Entity<AtividadeLog>()
                .HasOne(al => al.usuario)
                .WithMany(u => u.atividadeLogs)
                .HasForeignKey(al => al.id_usuario);

            modelBuilder.Entity<AtividadeLog>()
                .HasOne(al => al.material)
                .WithMany(m => m.atividadeLogs)
                .HasForeignKey(al => al.id_material);

            // AtividadeLogPS
            modelBuilder.Entity<AtividadeLogPS>()
                .HasOne(al => al.parteSolta)
                .WithMany(m => m.atividadeLogsPS)
                .HasForeignKey(al => al.id_parteSolta);


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

            modelBuilder.Entity<Permissao>()
            .ToTable("permissao");

            modelBuilder.Entity<AtividadeLog>()
                .ToTable("atividadeLog");

            modelBuilder.Entity<AtividadeLogPS>()
                .ToTable("atividadeLogPS");

            /* Fim - Tabela */
        }

    }
}