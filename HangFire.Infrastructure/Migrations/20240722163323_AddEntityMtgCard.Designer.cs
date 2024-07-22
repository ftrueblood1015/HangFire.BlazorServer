﻿// <auto-generated />
using System;
using HangFire.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HangFire.Infrastructure.Migrations
{
    [DbContext(typeof(HangFireBlazorServerDbContext))]
    [Migration("20240722163323_AddEntityMtgCard")]
    partial class AddEntityMtgCard
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HangFire.Domain.Entities.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Bath")
                        .HasColumnType("int");

                    b.Property<int>("Bed")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSold")
                        .HasColumnType("bit");

                    b.Property<int>("ListingPrice")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SquareFeet")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("HangFire.Domain.Entities.MtgCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ColorIdentity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ConvertedManaCost")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EdhrecRank")
                        .HasColumnType("int");

                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OracleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OracleText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PennyRank")
                        .HasColumnType("int");

                    b.Property<int?>("Power")
                        .HasColumnType("int");

                    b.Property<bool>("ProducesMana")
                        .HasColumnType("bit");

                    b.Property<string>("Rarity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScryfallUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Toughness")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("manaCost")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MtgCards");
                });
#pragma warning restore 612, 618
        }
    }
}
