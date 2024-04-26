﻿// <auto-generated />
using System;
using HiringSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HiringSystem.Infrastructure.Migrations
{
    [DbContext(typeof(HiringSystemDbContext))]
    [Migration("20240426022016_InitialCreatehostfixaggregateroot")]
    partial class InitialCreatehostfixaggregateroot
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("HiringSystem.Domain.Application.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("JobId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Supportive")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Applications", (string)null);
                });

            modelBuilder.Entity("HiringSystem.Domain.Job.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.Property<int>("JobType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TalentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TalentJobUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("WorkPlace")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TalentId");

                    b.ToTable("Jobs", (string)null);
                });

            modelBuilder.Entity("HiringSystem.Domain.Talent.Talent", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<string>("WebSite")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Talents", (string)null);
                });

            modelBuilder.Entity("HiringSystem.Domain.Application.Application", b =>
                {
                    b.HasOne("HiringSystem.Domain.Job.Job", null)
                        .WithMany("Applications")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HiringSystem.Domain.Job.Job", b =>
                {
                    b.HasOne("HiringSystem.Domain.Talent.Talent", "Talent")
                        .WithMany("Jobs")
                        .HasForeignKey("TalentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("HiringSystem.Domain.Job.ValueObjects.Salary", "Salary", b1 =>
                        {
                            b1.Property<Guid>("JobId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<decimal>("Maximum")
                                .HasColumnType("TEXT");

                            b1.Property<decimal>("Minimum")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Period")
                                .HasColumnType("INTEGER");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");
                        });

                    b.Navigation("Salary")
                        .IsRequired();

                    b.Navigation("Talent");
                });

            modelBuilder.Entity("HiringSystem.Domain.Job.Job", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("HiringSystem.Domain.Talent.Talent", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
