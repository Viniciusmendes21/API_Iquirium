﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SampleWebApi.Repository;

#nullable disable

namespace SampleWebApi.Migrations
{
    [DbContext(typeof(SampleContext))]
    partial class SampleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SampleWebApi.Models.FeedbackProduto", b =>
                {
                    b.Property<int>("IdFeedbackProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdFeedbackProduto"));

                    b.Property<string>("Comentario")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime>("DataEnvio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("IdTipoFeedbackProduto")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("IdUsuario")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.HasKey("IdFeedbackProduto");

                    b.HasIndex("IdTipoFeedbackProduto");

                    b.HasIndex("IdUsuario");

                    b.ToTable("tblFeedbackProduto", (string)null);
                });

            modelBuilder.Entity("SampleWebApi.Models.Perfil", b =>
                {
                    b.Property<int>("IdPerfil")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdPerfil"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("IdPerfil");

                    b.ToTable("tblPerfil", (string)null);
                });

            modelBuilder.Entity("SampleWebApi.Models.TipoFeedbackProduto", b =>
                {
                    b.Property<int>("IdTipoFeedbackProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdTipoFeedbackProduto"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("IdTipoFeedbackProduto");

                    b.ToTable("tblTipoFeedbackProduto", (string)null);
                });

            modelBuilder.Entity("SampleWebApi.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUsuario"));

                    b.Property<int>("IdPerfil")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("IdUsuario");

                    b.HasIndex("IdPerfil");

                    b.ToTable("tblUsuario", (string)null);
                });

            modelBuilder.Entity("SampleWebApi.Models.FeedbackProduto", b =>
                {
                    b.HasOne("SampleWebApi.Models.TipoFeedbackProduto", null)
                        .WithMany()
                        .HasForeignKey("IdTipoFeedbackProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SampleWebApi.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SampleWebApi.Models.Usuario", b =>
                {
                    b.HasOne("SampleWebApi.Models.Perfil", null)
                        .WithMany()
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
