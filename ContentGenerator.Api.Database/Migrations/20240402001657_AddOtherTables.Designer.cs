﻿// <auto-generated />
using System;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContentGenerator.Api.Database.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240402001657_AddOtherTables")]
    partial class AddOtherTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Assunto", b =>
                {
                    b.Property<int>("AssuntoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssuntoId"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataGeracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataPublicacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataValida")
                        .HasColumnType("datetime2");

                    b.Property<int>("DestinosId")
                        .HasColumnType("int");

                    b.Property<int>("HumorId")
                        .HasColumnType("int");

                    b.Property<string>("ImagemPost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IncluirImg")
                        .HasColumnType("bit");

                    b.Property<string>("ObjEveAssunto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostOriginal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostValidado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoAssuntoId")
                        .HasColumnType("int");

                    b.Property<int>("TipoValidacaoId")
                        .HasColumnType("int");

                    b.HasKey("AssuntoId");

                    b.HasIndex("DestinosId");

                    b.HasIndex("HumorId");

                    b.HasIndex("TipoAssuntoId");

                    b.HasIndex("TipoValidacaoId");

                    b.ToTable("Assunto");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.ContasEnvio", b =>
                {
                    b.Property<int>("ContasEnvioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContasEnvioId"));

                    b.Property<string>("Configuracao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DestinosId")
                        .HasColumnType("int");

                    b.HasKey("ContasEnvioId");

                    b.HasIndex("DestinosId");

                    b.ToTable("ContasEnvio");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Destinos", b =>
                {
                    b.Property<int>("DestinosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DestinosId"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DestinosId");

                    b.ToTable("Destinos");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmailId"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("EmailCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmailId");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Homenagem", b =>
                {
                    b.Property<int>("HomenagemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HomenagemId"));

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DestinosId")
                        .HasColumnType("int");

                    b.Property<int>("Dia")
                        .HasColumnType("int");

                    b.Property<int>("HumorId")
                        .HasColumnType("int");

                    b.Property<int>("Mes")
                        .HasColumnType("int");

                    b.Property<string>("ObjEveAssunto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoHomenagemId")
                        .HasColumnType("int");

                    b.Property<int>("TipoValidacaoId")
                        .HasColumnType("int");

                    b.HasKey("HomenagemId");

                    b.HasIndex("DestinosId");

                    b.HasIndex("HumorId");

                    b.HasIndex("TipoHomenagemId");

                    b.HasIndex("TipoValidacaoId");

                    b.ToTable("Homenagem");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Humor", b =>
                {
                    b.Property<int>("HumorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HumorId"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HumorId");

                    b.ToTable("Humor");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Publicacao", b =>
                {
                    b.Property<int>("PublicacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PublicacaoId"));

                    b.Property<int>("AssuntoId")
                        .HasColumnType("int");

                    b.HasKey("PublicacaoId");

                    b.HasIndex("AssuntoId");

                    b.ToTable("Publicacao");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.TipoAssunto", b =>
                {
                    b.Property<int>("TipoAssuntoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoAssuntoId"));

                    b.Property<string>("Assunto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoAssuntoId");

                    b.ToTable("TipoAssunto");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.TipoHomenagem", b =>
                {
                    b.Property<int>("TipoHomenagemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoHomenagemId"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoHomenagemId");

                    b.ToTable("TipoHomenagem");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.TipoValidacao", b =>
                {
                    b.Property<int>("TipoValidacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoValidacaoId"));

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoValidacaoId");

                    b.ToTable("TipoValidacao");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.WhatsApp", b =>
                {
                    b.Property<int>("WhatsAppId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WhatsAppId"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Numero_Fone")
                        .HasColumnType("bigint");

                    b.HasKey("WhatsAppId");

                    b.ToTable("WhatsApp");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Assunto", b =>
                {
                    b.HasOne("ContentGenerator.Api.Core.Entities.Destinos", "Destinos")
                        .WithMany()
                        .HasForeignKey("DestinosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContentGenerator.Api.Core.Entities.Humor", "Humor")
                        .WithMany()
                        .HasForeignKey("HumorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContentGenerator.Api.Core.Entities.TipoAssunto", "TipoAssunto")
                        .WithMany()
                        .HasForeignKey("TipoAssuntoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContentGenerator.Api.Core.Entities.TipoValidacao", "TipoValidacao")
                        .WithMany()
                        .HasForeignKey("TipoValidacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destinos");

                    b.Navigation("Humor");

                    b.Navigation("TipoAssunto");

                    b.Navigation("TipoValidacao");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.ContasEnvio", b =>
                {
                    b.HasOne("ContentGenerator.Api.Core.Entities.Destinos", "Destino")
                        .WithMany()
                        .HasForeignKey("DestinosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destino");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Homenagem", b =>
                {
                    b.HasOne("ContentGenerator.Api.Core.Entities.Destinos", "Destinos")
                        .WithMany()
                        .HasForeignKey("DestinosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContentGenerator.Api.Core.Entities.Humor", "Humor")
                        .WithMany()
                        .HasForeignKey("HumorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContentGenerator.Api.Core.Entities.TipoHomenagem", "TipoHomenagem")
                        .WithMany()
                        .HasForeignKey("TipoHomenagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContentGenerator.Api.Core.Entities.TipoValidacao", "TipoValidacao")
                        .WithMany()
                        .HasForeignKey("TipoValidacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destinos");

                    b.Navigation("Humor");

                    b.Navigation("TipoHomenagem");

                    b.Navigation("TipoValidacao");
                });

            modelBuilder.Entity("ContentGenerator.Api.Core.Entities.Publicacao", b =>
                {
                    b.HasOne("ContentGenerator.Api.Core.Entities.Assunto", "Assunto")
                        .WithMany()
                        .HasForeignKey("AssuntoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assunto");
                });
#pragma warning restore 612, 618
        }
    }
}
