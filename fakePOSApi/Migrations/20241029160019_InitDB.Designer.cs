﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fakePOSApi.Models;

#nullable disable

namespace fakePOSApi.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20241029160019_InitDB")]
    partial class InitDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("fakePOSApi.Models.Categoria", b =>
                {
                    b.Property<int>("IDCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDCategoria"));

                    b.Property<string>("CodCategoria")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("IDCategoria");

                    b.HasIndex("CodCategoria")
                        .IsUnique()
                        .HasFilter("[CodCategoria] IS NOT NULL");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("fakePOSApi.Models.Compra", b =>
                {
                    b.Property<int>("IDCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDCompra"));

                    b.Property<string>("Factura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date");

                    b.Property<int>("IDUser")
                        .HasColumnType("int");

                    b.Property<string>("MetodoPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalCompra")
                        .HasColumnType("float");

                    b.HasKey("IDCompra");

                    b.HasIndex("IDUser");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("fakePOSApi.Models.DetalleCompra", b =>
                {
                    b.Property<int>("IDDetalleCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDDetalleCompra"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IDCompra")
                        .HasColumnType("int");

                    b.Property<int>("IDProducto")
                        .HasColumnType("int");

                    b.Property<double>("SubTotal")
                        .HasColumnType("float");

                    b.HasKey("IDDetalleCompra");

                    b.HasIndex("IDCompra");

                    b.HasIndex("IDProducto");

                    b.ToTable("DetalleCompras");
                });

            modelBuilder.Entity("fakePOSApi.Models.DetalleVenta", b =>
                {
                    b.Property<int>("IDDetalleVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDDetalleVenta"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IDProducto")
                        .HasColumnType("int");

                    b.Property<int>("IDVenta")
                        .HasColumnType("int");

                    b.Property<double>("SubTotal")
                        .HasColumnType("float");

                    b.HasKey("IDDetalleVenta");

                    b.HasIndex("IDProducto");

                    b.HasIndex("IDVenta");

                    b.ToTable("DetalleVentas");
                });

            modelBuilder.Entity("fakePOSApi.Models.Kardex", b =>
                {
                    b.Property<int>("IDKardex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDKardex"));

                    b.Property<int>("Entrada")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date");

                    b.Property<int>("IDProducto")
                        .HasColumnType("int");

                    b.Property<int>("IDUser")
                        .HasColumnType("int");

                    b.Property<string>("NumDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salida")
                        .HasColumnType("int");

                    b.Property<string>("TipoMovimiento")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDKardex");

                    b.HasIndex("IDProducto");

                    b.HasIndex("IDUser");

                    b.ToTable("Kardex");
                });

            modelBuilder.Entity("fakePOSApi.Models.Producto", b =>
                {
                    b.Property<int>("IDProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDProducto"));

                    b.Property<string>("CodProducto")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDCategoria")
                        .HasColumnType("int");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("IDProducto");

                    b.HasIndex("CodProducto")
                        .IsUnique()
                        .HasFilter("[CodProducto] IS NOT NULL");

                    b.HasIndex("IDCategoria");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("fakePOSApi.Models.Usuario", b =>
                {
                    b.Property<int>("IDUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDUser"));

                    b.Property<string>("CodUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDUser");

                    b.HasIndex("CodUser")
                        .IsUnique()
                        .HasFilter("[CodUser] IS NOT NULL");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("fakePOSApi.Models.Venta", b =>
                {
                    b.Property<int>("IDVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDVenta"));

                    b.Property<string>("Factura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date");

                    b.Property<int>("IDUser")
                        .HasColumnType("int");

                    b.Property<bool>("IsContable")
                        .HasColumnType("bit");

                    b.Property<string>("MetodoPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalVenta")
                        .HasColumnType("float");

                    b.HasKey("IDVenta");

                    b.HasIndex("IDUser");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("fakePOSApi.Models.Compra", b =>
                {
                    b.HasOne("fakePOSApi.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IDUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("fakePOSApi.Models.DetalleCompra", b =>
                {
                    b.HasOne("fakePOSApi.Models.Compra", "Compra")
                        .WithMany()
                        .HasForeignKey("IDCompra")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fakePOSApi.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IDProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("fakePOSApi.Models.DetalleVenta", b =>
                {
                    b.HasOne("fakePOSApi.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IDProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fakePOSApi.Models.Venta", "Venta")
                        .WithMany()
                        .HasForeignKey("IDVenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("fakePOSApi.Models.Kardex", b =>
                {
                    b.HasOne("fakePOSApi.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IDProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fakePOSApi.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IDUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("fakePOSApi.Models.Producto", b =>
                {
                    b.HasOne("fakePOSApi.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("IDCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("fakePOSApi.Models.Venta", b =>
                {
                    b.HasOne("fakePOSApi.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IDUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
