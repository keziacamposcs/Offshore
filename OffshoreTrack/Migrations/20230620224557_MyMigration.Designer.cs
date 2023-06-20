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
    [Migration("20230620224557_MyMigration")]
    partial class MyMigration
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

                    b.Property<string>("manutencao")
                        .HasColumnType("TEXT");

                    b.Property<string>("tipo")
                        .HasColumnType("TEXT");

                    b.HasKey("id_manutencao");

                    b.HasIndex("id_criticidade");

                    b.HasIndex("id_fornecedor");

                    b.HasIndex("id_material");

                    b.HasIndex("id_setor");

                    b.ToTable("manutencao", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.Material", b =>
                {
                    b.Property<int>("id_material")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("anexo")
                        .HasColumnType("TEXT");

                    b.Property<string>("cod_barra")
                        .HasColumnType("TEXT");

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

                    b.Property<int?>("id_setor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_tipo")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("id_usuario")
                        .HasColumnType("INTEGER");

                    b.Property<string>("material")
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

                    b.ToTable("material", (string)null);
                });

            modelBuilder.Entity("OffshoreTrack.Models.OrdemCompra", b =>
                {
                    b.Property<int>("id_oc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly?>("data_compra")
                        .HasColumnType("TEXT");

                    b.Property<string>("oc")
                        .HasColumnType("TEXT");

                    b.Property<double?>("valor")
                        .HasColumnType("REAL");

                    b.HasKey("id_oc");

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

                    b.Property<string>("partesolta")
                        .HasColumnType("TEXT");

                    b.Property<int?>("quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("id_partesolta");

                    b.HasIndex("id_material");

                    b.HasIndex("id_oc");

                    b.ToTable("parteSolta", (string)null);
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

                    b.Property<int?>("valor1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("valor2")
                        .HasColumnType("INTEGER");

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

                    b.Property<string>("nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("senha")
                        .HasColumnType("TEXT");

                    b.Property<string>("usuario")
                        .HasColumnType("TEXT");

                    b.HasKey("id_usuario");

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

                    b.Navigation("criticidade");

                    b.Navigation("fornecedor");

                    b.Navigation("material");

                    b.Navigation("setor");
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

                    b.Navigation("cliente");

                    b.Navigation("criticidade");

                    b.Navigation("fornecedor");

                    b.Navigation("local");

                    b.Navigation("setor");

                    b.Navigation("tipo");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("OffshoreTrack.Models.ParteSolta", b =>
                {
                    b.HasOne("OffshoreTrack.Models.Material", "material")
                        .WithMany("parteSoltas")
                        .HasForeignKey("id_material");

                    b.HasOne("OffshoreTrack.Models.OrdemCompra", "oc")
                        .WithMany("parteSoltas")
                        .HasForeignKey("id_oc");

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
                    b.Navigation("manutencaos");

                    b.Navigation("materials");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Local", b =>
                {
                    b.Navigation("materials");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Material", b =>
                {
                    b.Navigation("manutencaos");

                    b.Navigation("parteSoltas");
                });

            modelBuilder.Entity("OffshoreTrack.Models.OrdemCompra", b =>
                {
                    b.Navigation("parteSoltas");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Setor", b =>
                {
                    b.Navigation("manutencaos");

                    b.Navigation("materials");

                    b.Navigation("rateios1");

                    b.Navigation("rateios2");
                });

            modelBuilder.Entity("OffshoreTrack.Models.Tipo", b =>
                {
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
