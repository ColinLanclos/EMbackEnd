﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backEnd_EM.Models;

#nullable disable

namespace backEnd_EM.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20231229183754_secondTry")]
    partial class secondTry
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("backEnd_EM.Properties.Models.Analyse", b =>
                {
                    b.Property<int>("AnalyseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AnalyseId"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("integer");

                    b.Property<int?>("AthletesAthleteId")
                        .HasColumnType("integer");

                    b.Property<string>("BreakDown")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("videoURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AnalyseId");

                    b.HasIndex("AthletesAthleteId");

                    b.ToTable("Analyses");
                });

            modelBuilder.Entity("backEnd_EM.Properties.Models.Appointments", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AppointmentId"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("integer");

                    b.Property<int?>("AthletesAthleteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Time")
                        .HasColumnType("real");

                    b.Property<string>("TypeApointment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AppointmentId");

                    b.HasIndex("AthletesAthleteId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("backEnd_EM.Properties.Models.Athletes", b =>
                {
                    b.Property<int>("AthleteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AthleteId"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Classification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Guardian")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Height")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("AthleteId");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("backEnd_EM.Properties.Models.ProgressTracker", b =>
                {
                    b.Property<int>("ProgressTrackerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProgressTrackerId"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("integer");

                    b.Property<int?>("AthletesAthleteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Value_Dor_Date")
                        .HasColumnType("real");

                    b.Property<string>("WorkOut")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProgressTrackerId");

                    b.HasIndex("AthletesAthleteId");

                    b.ToTable("ProgressTrackers");
                });

            modelBuilder.Entity("backEnd_EM.Properties.Models.Analyse", b =>
                {
                    b.HasOne("backEnd_EM.Properties.Models.Athletes", null)
                        .WithMany("Analyses")
                        .HasForeignKey("AthletesAthleteId");
                });

            modelBuilder.Entity("backEnd_EM.Properties.Models.Appointments", b =>
                {
                    b.HasOne("backEnd_EM.Properties.Models.Athletes", null)
                        .WithMany("Appointments")
                        .HasForeignKey("AthletesAthleteId");
                });

            modelBuilder.Entity("backEnd_EM.Properties.Models.ProgressTracker", b =>
                {
                    b.HasOne("backEnd_EM.Properties.Models.Athletes", null)
                        .WithMany("ProgressTrackers")
                        .HasForeignKey("AthletesAthleteId");
                });

            modelBuilder.Entity("backEnd_EM.Properties.Models.Athletes", b =>
                {
                    b.Navigation("Analyses");

                    b.Navigation("Appointments");

                    b.Navigation("ProgressTrackers");
                });
#pragma warning restore 612, 618
        }
    }
}
