﻿// <auto-generated />
using System;
using ISIS_PROJEKAT.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ISIS_PROJEKAT.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ISIS_PROJEKAT.Models.LoadDataHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("CloudCover")
                        .HasColumnType("float");

                    b.Property<string>("Conditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Dew")
                        .HasColumnType("float");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("FeelsLike")
                        .HasColumnType("float");

                    b.Property<double?>("Humidity")
                        .HasColumnType("float");

                    b.Property<double?>("Load")
                        .HasColumnType("float");

                    b.Property<string>("PTID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Precip")
                        .HasColumnType("float");

                    b.Property<double?>("SeaLevelPressure")
                        .HasColumnType("float");

                    b.Property<double?>("Snow")
                        .HasColumnType("float");

                    b.Property<double?>("SnowDepth")
                        .HasColumnType("float");

                    b.Property<double?>("Temperature")
                        .HasColumnType("float");

                    b.Property<string>("TimeZone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("UVIndex")
                        .HasColumnType("float");

                    b.Property<double?>("Visibilty")
                        .HasColumnType("float");

                    b.Property<double?>("WindDir")
                        .HasColumnType("float");

                    b.Property<double?>("WindGust")
                        .HasColumnType("float");

                    b.Property<double?>("WindSpeed")
                        .HasColumnType("float");

                    b.Property<bool>("isWeekend")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("LoadDatasHistory");
                });

            modelBuilder.Entity("ISIS_PROJEKAT.Models.LoadDataPrediction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Load")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("LoadDatasPrediction");
                });

            modelBuilder.Entity("ISIS_PROJEKAT.Models.WheatherForecast", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("CloudCover")
                        .HasColumnType("float");

                    b.Property<string>("Conditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Dew")
                        .HasColumnType("float");

                    b.Property<double?>("FeelsLike")
                        .HasColumnType("float");

                    b.Property<double?>("Humidity")
                        .HasColumnType("float");

                    b.Property<double?>("Precip")
                        .HasColumnType("float");

                    b.Property<double?>("SeaLevelPressure")
                        .HasColumnType("float");

                    b.Property<double?>("Snow")
                        .HasColumnType("float");

                    b.Property<double?>("SnowDepth")
                        .HasColumnType("float");

                    b.Property<double?>("Temperature")
                        .HasColumnType("float");

                    b.Property<double?>("UVIndex")
                        .HasColumnType("float");

                    b.Property<double?>("Visibilty")
                        .HasColumnType("float");

                    b.Property<double?>("WindDir")
                        .HasColumnType("float");

                    b.Property<double?>("WindGust")
                        .HasColumnType("float");

                    b.Property<double?>("WindSpeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WheatherForecasts");
                });
#pragma warning restore 612, 618
        }
    }
}
