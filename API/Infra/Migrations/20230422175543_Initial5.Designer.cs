﻿// <auto-generated />
using System;
using Infra.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230422175543_Initial5")]
    partial class Initial5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnName("City");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnName("Country");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CRIADO_EM");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("ATIVO");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnName("State");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("ULTIMA_ATUALZIACAO_EM");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id", "IsActive" }, "ix_id_active");

                    b.ToTable("Addresses", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CRIADO_EM");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("ATIVO");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("ULTIMA_ATUALZIACAO_EM");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PetId", "IsActive" }, "ix_id_active")
                        .HasDatabaseName("ix_id_active1");

                    b.ToTable("Images", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Age")
                        .HasColumnType("BIGINT")
                        .HasColumnName("Age");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CRIADO_EM");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("ATIVO");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnName("Name");

                    b.Property<int?>("NewOwnerId")
                        .HasColumnType("int");

                    b.Property<int>("OldOwnerId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int")
                        .HasColumnName("Size");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("Type");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("ULTIMA_ATUALZIACAO_EM");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("NewOwnerId");

                    b.HasIndex("OldOwnerId");

                    b.ToTable("Pets", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CRIADO_EM");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("ATIVO");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(600)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(70)")
                        .HasColumnName("Password");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("ULTIMA_ATUALZIACAO_EM");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.HasOne("Domain.Entities.Pet", "Pet")
                        .WithMany("Images")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Domain.Entities.Pet", b =>
                {
                    b.HasOne("Domain.Entities.User", "NewOwner")
                        .WithMany("PetsAdopted")
                        .HasForeignKey("NewOwnerId");

                    b.HasOne("Domain.Entities.User", "OldOwner")
                        .WithMany("PetsToAdopt")
                        .HasForeignKey("OldOwnerId")
                        .IsRequired();

                    b.Navigation("NewOwner");

                    b.Navigation("OldOwner");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.Address", "Address")
                        .WithOne("User")
                        .HasForeignKey("Domain.Entities.User", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Domain.Entities.Address", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Pet", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("PetsAdopted");

                    b.Navigation("PetsToAdopt");
                });
#pragma warning restore 612, 618
        }
    }
}
