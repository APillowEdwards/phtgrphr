﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhtgrphrAPI.DbContexts;

namespace PhtgrphrAPI.Migrations
{
    [DbContext(typeof(PhtgrphrContext))]
    partial class PhtgrphrContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PhtgrphrAPI.Models.Gallery", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("Guid")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<bool>("ShowDownloadAll")
                        .HasColumnType("bit");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("PhtgrphrAPI.Models.GalleryAccessToken", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Expiry")
                        .HasColumnType("datetime");

                    b.Property<int?>("GalleryID")
                        .HasColumnType("int");

                    b.Property<byte[]>("Token")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.HasKey("ID");

                    b.HasIndex("GalleryID");

                    b.ToTable("GalleryAccessTokens");
                });

            modelBuilder.Entity("PhtgrphrAPI.Models.Image", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BlurHash")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<int?>("GalleryID")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GalleryID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("PhtgrphrAPI.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PhtgrphrAPI.Models.UserAccessToken", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Expiry")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("Token")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("UserAccessTokens");
                });

            modelBuilder.Entity("PhtgrphrAPI.Models.Gallery", b =>
                {
                    b.HasOne("PhtgrphrAPI.Models.User", "User")
                        .WithMany("Galleries")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("PhtgrphrAPI.Models.GalleryAccessToken", b =>
                {
                    b.HasOne("PhtgrphrAPI.Models.Gallery", "Gallery")
                        .WithMany("GalleryAccessTokens")
                        .HasForeignKey("GalleryID");
                });

            modelBuilder.Entity("PhtgrphrAPI.Models.Image", b =>
                {
                    b.HasOne("PhtgrphrAPI.Models.Gallery", "Gallery")
                        .WithMany("Images")
                        .HasForeignKey("GalleryID");
                });

            modelBuilder.Entity("PhtgrphrAPI.Models.UserAccessToken", b =>
                {
                    b.HasOne("PhtgrphrAPI.Models.User", "User")
                        .WithMany("UserAccessTokens")
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}
