﻿// <auto-generated />
using System;
using HotOrCold.Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotOrCold.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240309115602_m2")]
    partial class m2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryDrink", b =>
                {
                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("DrinksDrinkId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesCategoryId", "DrinksDrinkId");

                    b.HasIndex("DrinksDrinkId");

                    b.ToTable("CategoryDrink");
                });

            modelBuilder.Entity("HotOrCold.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("HotOrCold.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<int>("CategoryName")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HotOrCold.Entities.Command", b =>
                {
                    b.Property<int>("CommandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommandId"));

                    b.Property<DateOnly>("CommandDate")
                        .HasColumnType("date");

                    b.Property<int>("CommandStatus")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("CommandId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("HotOrCold.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("HotOrCold.Entities.Drink", b =>
                {
                    b.Property<int>("DrinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DrinkId"));

                    b.Property<int>("Drinktype")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerLiter")
                        .HasColumnType("float");

                    b.HasKey("DrinkId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("HotOrCold.Entities.DrinkCopy", b =>
                {
                    b.Property<int>("DrinkCopyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DrinkCopyId"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<int?>("CommandId")
                        .HasColumnType("int");

                    b.Property<int?>("DrinkId1")
                        .HasColumnType("int");

                    b.Property<double>("QuantityInLiter")
                        .HasColumnType("float");

                    b.HasKey("DrinkCopyId");

                    b.HasIndex("CartId");

                    b.HasIndex("CommandId");

                    b.HasIndex("DrinkId1");

                    b.ToTable("DrinkCopies");
                });

            modelBuilder.Entity("CategoryDrink", b =>
                {
                    b.HasOne("HotOrCold.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotOrCold.Entities.Drink", null)
                        .WithMany()
                        .HasForeignKey("DrinksDrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HotOrCold.Entities.Cart", b =>
                {
                    b.HasOne("HotOrCold.Entities.Customer", null)
                        .WithOne("Cart")
                        .HasForeignKey("HotOrCold.Entities.Cart", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HotOrCold.Entities.Command", b =>
                {
                    b.HasOne("HotOrCold.Entities.Customer", null)
                        .WithMany("Commands")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HotOrCold.Entities.DrinkCopy", b =>
                {
                    b.HasOne("HotOrCold.Entities.Cart", null)
                        .WithMany("DrinkCopies")
                        .HasForeignKey("CartId");

                    b.HasOne("HotOrCold.Entities.Command", null)
                        .WithMany("DrinkCopies")
                        .HasForeignKey("CommandId");

                    b.HasOne("HotOrCold.Entities.Drink", null)
                        .WithMany("DrinkCopies")
                        .HasForeignKey("DrinkId1");
                });

            modelBuilder.Entity("HotOrCold.Entities.Cart", b =>
                {
                    b.Navigation("DrinkCopies");
                });

            modelBuilder.Entity("HotOrCold.Entities.Command", b =>
                {
                    b.Navigation("DrinkCopies");
                });

            modelBuilder.Entity("HotOrCold.Entities.Customer", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Commands");
                });

            modelBuilder.Entity("HotOrCold.Entities.Drink", b =>
                {
                    b.Navigation("DrinkCopies");
                });
#pragma warning restore 612, 618
        }
    }
}