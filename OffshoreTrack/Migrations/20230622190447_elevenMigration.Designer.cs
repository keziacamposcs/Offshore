﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OffshoreTrack.Data;

#nullable disable

namespace OffshoreTrack.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20230622190447_elevenMigration")]
    partial class elevenMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("OffshoreTrack.Models.Cliente", b =>
                {
                    b.Property<int>("id_cliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cliente")
                        .HasColumnType("TEXT");

                    b.Property<string>("cnpj")
                        .HasColumnType("TEXT");

                    b.HasKey("id_cliente");

                    b.ToTable("cliente", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Criticidade", b =>
                {
                    b.Property<int>("id_criticidade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("criticidade")
                        .HasColumnType("TEXT");

                    b.HasKey("id_criticidade");

                    b.ToTable("criticidade", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Fornecedor", b =>
                {
                    b.Property<int>("id_fornecedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cnpj")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<string>("endereco")
                        .HasColumnType("TEXT");

                    b.Property<string>("fornecedor")
                        .HasColumnType("TEXT");

                    b.Property<string>("telefone")
                        .HasColumnType("TEXT");

                    b.Property<string>("vendedor")
                        .HasColumnType("TEXT");

                    b.HasKey("id_fornecedor");

                    b.ToTable("fornecedor", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Local", b =>
                {
                    b.Property<int>("id_local")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_cliente")
                        .HasColumnType("INTEGER");

                    b.Property<string>("local")
                        .HasColumnType("TEXT");

                    b.HasKey("id_local");

                    b.HasIndex("id_cliente");

                    b.ToTable("local", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Manutencao", b =>
                {
                    b.Property<int>("id_manutencao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("data")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("data_conclusao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("data_prevista")
                        .HasColumnType("TEXT");

                    b.Property<string>("descricao")
                        .HasColumnType("TEXT");

                    b.Property<int?>("id_criticidade")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_fornecedor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_material")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_setor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_status")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_tipo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("manutencao")
                        .HasColumnType("TEXT");

                    b.HasKey("id_manutencao");

                    b.HasIndex("id_criticidade");

                    b.HasIndex("id_fornecedor");

                    b.HasIndex("id_material");

                    b.HasIndex("id_setor");

                    b.HasIndex("id_status");

                    b.HasIndex("id_tipo");

                    b.ToTable("manutencao", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Material", b =>
                {
                    b.Property<int>("id_material")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("anexo")
                        .HasColumnType("BLOB");

                    b.Property<string>("descricao")
                        .HasColumnType("TEXT");

                    b.Property<int?>("id_cliente")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_criticidade")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_fornecedor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_local")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_manutencao")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_setor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_tipo")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_usuario")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("manutencaoid_manutencao")
                        .HasColumnType("INTEGER");

                    b.Property<string>("material")
                        .HasColumnType("TEXT");

                    b.Property<string>("numeroSerie")
                        .HasColumnType("TEXT");

                    b.Property<string>("qrcode")
                        .HasColumnType("TEXT");

                    b.Property<string>("tamanho")
                        .HasColumnType("TEXT");

                    b.HasKey("id_material");

                    b.HasIndex("id_cliente");

                    b.HasIndex("id_criticidade");

                    b.HasIndex("id_fornecedor");

                    b.HasIndex("id_local");

                    b.HasIndex("id_setor");

                    b.HasIndex("id_tipo");

                    b.HasIndex("id_usuario");

                    b.HasIndex("manutencaoid_manutencao");

                    b.ToTable("material", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.OrdemCompra", b =>
                {
                    b.Property<int>("id_oc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly?>("data_compra")
                        .HasColumnType("TEXT");

                    b.Property<int?>("id_fornecedor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_fornecedor2")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_fornecedor3")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_material")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_parteSolta")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_setor")
                        .HasColumnType("INTEGER");

                    b.Property<string>("oc")
                        .HasColumnType("TEXT");

                    b.Property<string>("prioridade")
                        .HasColumnType("TEXT");

                    b.Property<double?>("valor")
                        .HasColumnType("REAL");

                    b.HasKey("id_oc");

                    b.HasIndex("id_fornecedor");

                    b.HasIndex("id_fornecedor2");

                    b.HasIndex("id_fornecedor3");

                    b.HasIndex("id_material");

                    b.HasIndex("id_parteSolta");

                    b.HasIndex("id_setor");

                    b.ToTable("ordemCompra", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.ParteSolta", b =>
                {
                    b.Property<int>("id_partesolta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("anexo")
                        .HasColumnType("TEXT");

                    b.Property<int?>("id_material")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_oc")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ocid_oc")
                        .HasColumnType("INTEGER");

                    b.Property<string>("partesolta")
                        .HasColumnType("TEXT");

                    b.Property<int?>("quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("id_partesolta");

                    b.HasIndex("id_material");

                    b.HasIndex("ocid_oc");

                    b.ToTable("parteSolta", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Permissao", b =>
                {
                    b.Property<int>("id_permissao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("nome_permissao")
                        .HasColumnType("TEXT");

                    b.Property<bool>("permissao_admin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("pode_atualizar")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("pode_criar")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("pode_deletar")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("pode_ler")
                        .HasColumnType("INTEGER");

                    b.HasKey("id_permissao");

                    b.ToTable("permissao", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Rateio", b =>
                {
                    b.Property<int>("id_rateio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_setor1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_setor2")
                        .HasColumnType("INTEGER");

                    b.Property<string>("rateio")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("valor1")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("valor2")
                        .HasColumnType("TEXT");

                    b.HasKey("id_rateio");

                    b.HasIndex("id_setor1");

                    b.HasIndex("id_setor2");

                    b.ToTable("rateio", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Setor", b =>
                {
                    b.Property<int>("id_setor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("setor")
                        .HasColumnType("TEXT");

                    b.HasKey("id_setor");

                    b.ToTable("setor", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Status", b =>
                {
                    b.Property<int>("id_status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("status")
                        .HasColumnType("TEXT");

                    b.HasKey("id_status");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Tipo", b =>
                {
                    b.Property<int>("id_tipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("tipo")
                        .HasColumnType("TEXT");

                    b.HasKey("id_tipo");

                    b.ToTable("tipo", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Usuario", b =>
                {
                    b.Property<int>("id_usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cpf")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<int?>("id_permissao")
                        .HasColumnType("INTEGER");

                    b.Property<string>("nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("senha")
                        .HasColumnType("TEXT");

                    b.Property<string>("usuario")
                        .HasColumnType("TEXT");

                    b.HasKey("id_usuario");

                    b.HasIndex("id_permissao");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Local", b =>
                {
                    b.HasOne("OffshoreTrack.Models.Cliente", "Cliente")
                        .WithMany("locals")
                        .HasForeignKey("id_cliente");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Manutencao", b =>
                {
                    b.HasOne("OffshoreTrack.Models.Criticidade", "criticidade")
                        .WithMany("manutencaos")
                        .HasForeignKey("id_criticidade");

                    b.HasOne("OffshoreTrack.Models.Fornecedor", "fornecedor")
                        .WithMany("manutencaos")
                        .HasForeignKey("id_fornecedor");

                    b.HasOne("OffshoreTrack.Models.Material", "material")
                        .WithMany("manutencaos")
                        .HasForeignKey("id_material");

                    b.HasOne("OffshoreTrack.Models.Setor", "setor")
                        .WithMany("manutencaos")
                        .HasForeignKey("id_setor");

                    b.HasOne("OffshoreTrack.Models.Status", "status")
                        .WithMany("manutencaos")
                        .HasForeignKey("id_status");

                    b.HasOne("OffshoreTrack.Models.Tipo", "tipo")
                        .WithMany("manutencaos")
                        .HasForeignKey("id_tipo");

                    b.Navigation("criticidade");

                    b.Navigation("fornecedor");

                    b.Navigation("material");

                    b.Navigation("setor");

                    b.Navigation("status");

                    b.Navigation("tipo");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Material", b =>
                {
                    b.HasOne("OffshoreTrack.Models.Cliente", "cliente")
                        .WithMany("materials")
                        .HasForeignKey("id_cliente");

                    b.HasOne("OffshoreTrack.Models.Criticidade", "criticidade")
                        .WithMany("materials")
                        .HasForeignKey("id_criticidade");

                    b.HasOne("OffshoreTrack.Models.Fornecedor", "fornecedor")
                        .WithMany("materials")
                        .HasForeignKey("id_fornecedor");

                    b.HasOne("OffshoreTrack.Models.Local", "local")
                        .WithMany("materials")
                        .HasForeignKey("id_local");

                    b.HasOne("OffshoreTrack.Models.Setor", "setor")
                        .WithMany("materials")
                        .HasForeignKey("id_setor");

                    b.HasOne("OffshoreTrack.Models.Tipo", "tipo")
                        .WithMany("materials")
                        .HasForeignKey("id_tipo");

                    b.HasOne("OffshoreTrack.Models.Usuario", "usuario")
                        .WithMany("materials")
                        .HasForeignKey("id_usuario");

                    b.HasOne("OffshoreTrack.Models.Manutencao", "manutencao")
                        .WithMany("materials")
                        .HasForeignKey("manutencaoid_manutencao");

                    b.Navigation("cliente");

                    b.Navigation("criticidade");

                    b.Navigation("fornecedor");

                    b.Navigation("local");

                    b.Navigation("manutencao");

                    b.Navigation("setor");

                    b.Navigation("tipo");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("OffshoreTrack.Models.OrdemCompra", b =>
                {
                    b.HasOne("OffshoreTrack.Models.Fornecedor", "fornecedor")
                        .WithMany("OrdensCompra1")
                        .HasForeignKey("id_fornecedor");

                    b.HasOne("OffshoreTrack.Models.Fornecedor", "fornecedor2")
                        .WithMany("OrdensCompra2")
                        .HasForeignKey("id_fornecedor2");

                    b.HasOne("OffshoreTrack.Models.Fornecedor", "fornecedor3")
                        .WithMany("OrdensCompra3")
                        .HasForeignKey("id_fornecedor3");

                    b.HasOne("OffshoreTrack.Models.Material", "material")
                        .WithMany("ordemCompras")
                        .HasForeignKey("id_material");

                    b.HasOne("OffshoreTrack.Models.ParteSolta", "parteSolta")
                        .WithMany("ordemCompras")
                        .HasForeignKey("id_parteSolta");

                    b.HasOne("OffshoreTrack.Models.Setor", "setor")
                        .WithMany("ordemCompras")
                        .HasForeignKey("id_setor");

                    b.Navigation("fornecedor");

                    b.Navigation("fornecedor2");

                    b.Navigation("fornecedor3");

                    b.Navigation("material");

                    b.Navigation("parteSolta");

                    b.Navigation("setor");
                });

            modelBuilder.Entity("OffshoreTrack.Models.ParteSolta", b =>
                {
                    b.HasOne("OffshoreTrack.Models.Material", "material")
                        .WithMany("parteSoltas")
                        .HasForeignKey("id_material");

                    b.HasOne("OffshoreTrack.Models.OrdemCompra", "oc")
                        .WithMany("parteSoltas")
                        .HasForeignKey("ocid_oc");

                    b.Navigation("material");

                    b.Navigation("oc");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Rateio", b =>
                {
                    b.HasOne("OffshoreTrack.Models.Setor", "setor1")
                        .WithMany("rateios1")
                        .HasForeignKey("id_setor1");

                    b.HasOne("OffshoreTrack.Models.Setor", "setor2")
                        .WithMany("rateios2")
                        .HasForeignKey("id_setor2");

                    b.Navigation("setor1");

                    b.Navigation("setor2");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Usuario", b =>
                {
                    b.HasOne("OffshoreTrack.Models.Permissao", "Permissao")
                        .WithMany("usuarios")
                        .HasForeignKey("id_permissao");

                    b.Navigation("Permissao");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Cliente", b =>
                {
                    b.Navigation("locals");

                    b.Navigation("materials");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Criticidade", b =>
                {
                    b.Navigation("manutencaos");

                    b.Navigation("materials");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Fornecedor", b =>
                {
                    b.Navigation("OrdensCompra1");

                    b.Navigation("OrdensCompra2");

                    b.Navigation("OrdensCompra3");

                    b.Navigation("manutencaos");

                    b.Navigation("materials");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Local", b =>
                {
                    b.Navigation("materials");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Manutencao", b =>
                {
                    b.Navigation("materials");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Material", b =>
                {
                    b.Navigation("manutencaos");

                    b.Navigation("ordemCompras");

                    b.Navigation("parteSoltas");
                });

            modelBuilder.Entity("OffshoreTrack.Models.OrdemCompra", b =>
                {
                    b.Navigation("parteSoltas");
                });

            modelBuilder.Entity("OffshoreTrack.Models.ParteSolta", b =>
                {
                    b.Navigation("ordemCompras");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Permissao", b =>
                {
                    b.Navigation("usuarios");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Setor", b =>
                {
                    b.Navigation("manutencaos");

                    b.Navigation("materials");

                    b.Navigation("ordemCompras");

                    b.Navigation("rateios1");

                    b.Navigation("rateios2");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Status", b =>
                {
                    b.Navigation("manutencaos");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Tipo", b =>
                {
                    b.Navigation("manutencaos");

                    b.Navigation("materials");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Usuario", b =>
                {
                    b.Navigation("materials");
                });
#pragma warning restore 612, 618
        }
    }
}
