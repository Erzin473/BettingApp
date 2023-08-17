﻿// <auto-generated />
using System;
using BettingWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BettingWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230815174313_nullOdss")]
    partial class nullOdss
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BettingWebApp.Models.Bet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("IsLive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MatchID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("MatchID");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("BettingWebApp.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CategoryID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsLive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("BettingWebApp.Models.Match", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("EventID")
                        .HasColumnType("int");

                    b.Property<string>("MatchType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EventID");

                    b.ToTable("Matchs");
                });

            modelBuilder.Entity("BettingWebApp.Models.Odd", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BetID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialBetValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BetID");

                    b.ToTable("Odds");
                });

            modelBuilder.Entity("BettingWebApp.Models.Bet", b =>
                {
                    b.HasOne("BettingWebApp.Models.Match", null)
                        .WithMany("Bet")
                        .HasForeignKey("MatchID");
                });

            modelBuilder.Entity("BettingWebApp.Models.Match", b =>
                {
                    b.HasOne("BettingWebApp.Models.Event", null)
                        .WithMany("Match")
                        .HasForeignKey("EventID");
                });

            modelBuilder.Entity("BettingWebApp.Models.Odd", b =>
                {
                    b.HasOne("BettingWebApp.Models.Bet", null)
                        .WithMany("Odd")
                        .HasForeignKey("BetID");
                });

            modelBuilder.Entity("BettingWebApp.Models.Bet", b =>
                {
                    b.Navigation("Odd");
                });

            modelBuilder.Entity("BettingWebApp.Models.Event", b =>
                {
                    b.Navigation("Match");
                });

            modelBuilder.Entity("BettingWebApp.Models.Match", b =>
                {
                    b.Navigation("Bet");
                });
#pragma warning restore 612, 618
        }
    }
}
