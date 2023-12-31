﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230617074847_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Coverage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<double>("InsuranceCoefficient")
                        .HasColumnType("float");

                    b.Property<decimal>("MaximumFund")
                        .HasColumnType("money");

                    b.Property<decimal>("MinimumFund")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Coverages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Surgical Coverage",
                            InsuranceCoefficient = 0.0051999999999999998,
                            MaximumFund = 500000000m,
                            MinimumFund = 5000m,
                            Name = "Surgical",
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "Dental Coverage",
                            InsuranceCoefficient = 0.0041999999999999997,
                            MaximumFund = 400000000m,
                            MinimumFund = 4000m,
                            Name = "Dental",
                            Type = 2
                        },
                        new
                        {
                            Id = 3,
                            Description = "Hospitalization Coverage",
                            InsuranceCoefficient = 0.0050000000000000001,
                            MaximumFund = 200000000m,
                            MinimumFund = 2000m,
                            Name = "Hospitalization",
                            Type = 3
                        });
                });

            modelBuilder.Entity("Domain.Entities.Inquiry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Inquiries");
                });

            modelBuilder.Entity("Domain.Entities.InquiryCoverage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoverageId")
                        .HasColumnType("int");

                    b.Property<int>("InquiryId")
                        .HasColumnType("int");

                    b.Property<decimal>("RequestedFund")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CoverageId");

                    b.HasIndex("InquiryId");

                    b.ToTable("InquiryCoverages");
                });

            modelBuilder.Entity("Domain.Entities.InquiryCoverage", b =>
                {
                    b.HasOne("Domain.Entities.Coverage", "Coverage")
                        .WithMany()
                        .HasForeignKey("CoverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Inquiry", "Inquiry")
                        .WithMany("InquiryCoverages")
                        .HasForeignKey("InquiryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coverage");

                    b.Navigation("Inquiry");
                });

            modelBuilder.Entity("Domain.Entities.Inquiry", b =>
                {
                    b.Navigation("InquiryCoverages");
                });
#pragma warning restore 612, 618
        }
    }
}
