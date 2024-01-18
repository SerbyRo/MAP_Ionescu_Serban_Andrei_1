﻿// <auto-generated />
using System;
using MAP_Ionescu_Serban_Andrei.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MAP_Ionescu_Serban_Andrei.Migrations
{
    [DbContext(typeof(BasketballContext))]
    [Migration("20240117124320_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Baller", b =>
                {
                    b.Property<int>("BallerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BallerID"));

                    b.Property<string>("BallerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TShirtNumber")
                        .HasColumnType("int");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("BallerID");

                    b.HasIndex("TeamID");

                    b.ToTable("Baller", (string)null);
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Coach", b =>
                {
                    b.Property<int>("CoachID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CoachID"));

                    b.Property<string>("CoachCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoachName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonalStatsID")
                        .HasColumnType("int");

                    b.Property<int>("debutYear")
                        .HasColumnType("int");

                    b.HasKey("CoachID");

                    b.HasIndex("PersonalStatsID");

                    b.ToTable("Coach", (string)null);
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.GamePlan", b =>
                {
                    b.Property<int>("GamePlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GamePlanID"));

                    b.Property<int>("BallerID")
                        .HasColumnType("int");

                    b.Property<int>("CoachID")
                        .HasColumnType("int");

                    b.HasKey("GamePlanID");

                    b.HasIndex("BallerID");

                    b.HasIndex("CoachID");

                    b.ToTable("GamePlan", (string)null);
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Match", b =>
                {
                    b.Property<int>("MatchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchID"));

                    b.Property<int>("markedPoints")
                        .HasColumnType("int");

                    b.Property<int>("minutesPlayed")
                        .HasColumnType("int");

                    b.Property<string>("oppositeTeam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MatchID");

                    b.ToTable("Match", (string)null);
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.PersonalStats", b =>
                {
                    b.Property<int>("PersonalStatsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonalStatsID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("overallScore")
                        .HasColumnType("real");

                    b.HasKey("PersonalStatsID");

                    b.ToTable("PersonalStats");
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Position", b =>
                {
                    b.Property<int>("BallerID")
                        .HasColumnType("int");

                    b.Property<int>("MatchID")
                        .HasColumnType("int");

                    b.HasKey("BallerID", "MatchID");

                    b.HasIndex("MatchID");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamID"));

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamID");

                    b.ToTable("Team", (string)null);
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Baller", b =>
                {
                    b.HasOne("MAP_Ionescu_Serban_Andrei.Models.Team", "Team")
                        .WithMany("Ballers")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Coach", b =>
                {
                    b.HasOne("MAP_Ionescu_Serban_Andrei.Models.PersonalStats", "PersonalStats")
                        .WithMany()
                        .HasForeignKey("PersonalStatsID");

                    b.Navigation("PersonalStats");
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.GamePlan", b =>
                {
                    b.HasOne("MAP_Ionescu_Serban_Andrei.Models.Baller", "Baller")
                        .WithMany("GamePlanes")
                        .HasForeignKey("BallerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MAP_Ionescu_Serban_Andrei.Models.Coach", "Coach")
                        .WithMany("GamePlans")
                        .HasForeignKey("CoachID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Baller");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Position", b =>
                {
                    b.HasOne("MAP_Ionescu_Serban_Andrei.Models.Baller", "Baller")
                        .WithMany("Positions")
                        .HasForeignKey("BallerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MAP_Ionescu_Serban_Andrei.Models.Match", "Match")
                        .WithMany("Positions")
                        .HasForeignKey("MatchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Baller");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Baller", b =>
                {
                    b.Navigation("GamePlanes");

                    b.Navigation("Positions");
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Coach", b =>
                {
                    b.Navigation("GamePlans");
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Match", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("MAP_Ionescu_Serban_Andrei.Models.Team", b =>
                {
                    b.Navigation("Ballers");
                });
#pragma warning restore 612, 618
        }
    }
}