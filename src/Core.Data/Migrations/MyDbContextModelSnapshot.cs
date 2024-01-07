﻿// <auto-generated />
using System;
using Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Core.Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Business.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Complement")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("char(8)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("DateTime");

                    b.HasKey("Id");

                    b.ToTable("Adresses", (string)null);
                });

            modelBuilder.Entity("Core.Business.Models.AppAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionName")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("ControllerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("DateTime");

                    b.HasKey("Id");

                    b.HasIndex("ControllerId");

                    b.ToTable("Actions", (string)null);
                });

            modelBuilder.Entity("Core.Business.Models.AppController", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ControllerName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("DateTime");

                    b.HasKey("Id");

                    b.ToTable("Controllers", (string)null);
                });

            modelBuilder.Entity("Core.Business.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("DateTime");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Core.Business.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("1");

                    b.Property<string>("NIF")
                        .IsRequired()
                        .HasColumnType("char(14)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("DateTime");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.HasKey("Id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("Core.Business.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateRegister")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("DateTime");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Core.Business.Models.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("char(14)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TypeSupplier")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("DateTime");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Suppliers", (string)null);
                });

            modelBuilder.Entity("Core.Business.Models.AppAction", b =>
                {
                    b.HasOne("Core.Business.Models.AppController", "Controller")
                        .WithMany("Actions")
                        .HasForeignKey("ControllerId")
                        .IsRequired();

                    b.Navigation("Controller");
                });

            modelBuilder.Entity("Core.Business.Models.Category", b =>
                {
                    b.HasOne("Core.Business.Models.Category", "ParentCategory")
                        .WithMany("ParentCategories")
                        .HasForeignKey("CategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Core.Business.Models.Product", b =>
                {
                    b.HasOne("Core.Business.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId")
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Core.Business.Models.Supplier", b =>
                {
                    b.HasOne("Core.Business.Models.Address", "Address")
                        .WithOne("Supplier")
                        .HasForeignKey("Core.Business.Models.Supplier", "AddressId")
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Core.Business.Models.Address", b =>
                {
                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Core.Business.Models.AppController", b =>
                {
                    b.Navigation("Actions");
                });

            modelBuilder.Entity("Core.Business.Models.Category", b =>
                {
                    b.Navigation("ParentCategories");
                });

            modelBuilder.Entity("Core.Business.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
