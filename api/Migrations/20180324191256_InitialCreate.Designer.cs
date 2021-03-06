﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using rate_api.DataAccess.Models;
using System;

namespace rateapi.Migrations
{
    [DbContext(typeof(RatesContext))]
    [Migration("20180324191256_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("rate_api.DataAccess.Models.Rate", b =>
                {
                    b.Property<int>("RateId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DayOfWeek");

                    b.Property<int>("StartTime");

                    b.Property<int>("EndTime");

                    b.Property<double>("Price");

                    b.HasKey("RateId");

                    b.ToTable("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
