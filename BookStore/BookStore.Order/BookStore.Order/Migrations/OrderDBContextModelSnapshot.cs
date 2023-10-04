﻿// <auto-generated />
using BookStore.Order.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore.Order.Migrations
{
    [DbContext(typeof(OrderDBContext))]
    partial class OrderDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookStore.Order.Entity.OrderEntity", b =>
                {
                    b.Property<long>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderID"), 1L, 1);

                    b.Property<long>("BookID")
                        .HasColumnType("bigint");

                    b.Property<int>("OrderQty")
                        .HasColumnType("int");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("OrderID");

                    b.ToTable("Ordres");
                });

            modelBuilder.Entity("BookStore.Order.Entity.WishListEntity", b =>
                {
                    b.Property<long>("WishListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("WishListID"), 1L, 1);

                    b.Property<long>("BookID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("WishListID");

                    b.ToTable("WishLists");
                });
#pragma warning restore 612, 618
        }
    }
}
