﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shopping_Appilication.Models;

#nullable disable

namespace Shopping_Appilication.Migrations
{
    [DbContext(typeof(ShopDBContext))]
    [Migration("20230319021359_add_table_image_19032023")]
    partial class add_table_image_19032023
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Shopping_Appilication.Models.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("Datetime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("HoaDon", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.BillDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdHD")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSP")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdHD");

                    b.HasIndex("IdSP");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Cart", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("UserID");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.CartDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDSP")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IDSP");

                    b.HasIndex("UserID");

                    b.ToTable("CartDetails", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Color", b =>
                {
                    b.Property<Guid>("IdColor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdColor");

                    b.ToTable("Color", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Image", b =>
                {
                    b.Property<Guid>("IdImage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image1")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Image2")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Image3")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Image4")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdImage");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid?>("IdColor")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdImage")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdSize")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdSole")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("IdColor");

                    b.HasIndex("IdImage");

                    b.HasIndex("IdSize");

                    b.HasIndex("IdSole");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("RoleID");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Size", b =>
                {
                    b.Property<Guid>("IdSize")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdSize");

                    b.ToTable("Size", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Sole", b =>
                {
                    b.Property<Guid>("IdSole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Fabric")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdSole");

                    b.ToTable("Sole", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Bill", b =>
                {
                    b.HasOne("Shopping_Appilication.Models.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.BillDetail", b =>
                {
                    b.HasOne("Shopping_Appilication.Models.Bill", "Bill")
                        .WithMany("BillDetails")
                        .HasForeignKey("IdHD")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shopping_Appilication.Models.Product", "Product")
                        .WithMany("BillDetails")
                        .HasForeignKey("IdSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Cart", b =>
                {
                    b.HasOne("Shopping_Appilication.Models.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("Shopping_Appilication.Models.Cart", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.CartDetail", b =>
                {
                    b.HasOne("Shopping_Appilication.Models.Product", "Product")
                        .WithMany("CartDetails")
                        .HasForeignKey("IDSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Product");

                    b.HasOne("Shopping_Appilication.Models.Cart", "Cart")
                        .WithMany("CartDetails")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Cart");

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Product", b =>
                {
                    b.HasOne("Shopping_Appilication.Models.Color", "Color")
                        .WithMany("Products")
                        .HasForeignKey("IdColor");

                    b.HasOne("Shopping_Appilication.Models.Image", "Image")
                        .WithMany("Products")
                        .HasForeignKey("IdImage");

                    b.HasOne("Shopping_Appilication.Models.Size", "Size")
                        .WithMany("Products")
                        .HasForeignKey("IdSize");

                    b.HasOne("Shopping_Appilication.Models.Sole", "Sole")
                        .WithMany("Products")
                        .HasForeignKey("IdSole");

                    b.Navigation("Color");

                    b.Navigation("Image");

                    b.Navigation("Size");

                    b.Navigation("Sole");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.User", b =>
                {
                    b.HasOne("Shopping_Appilication.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Bill", b =>
                {
                    b.Navigation("BillDetails");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Cart", b =>
                {
                    b.Navigation("CartDetails");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Color", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Image", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Product", b =>
                {
                    b.Navigation("BillDetails");

                    b.Navigation("CartDetails");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Size", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.Sole", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shopping_Appilication.Models.User", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Cart")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
