﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Migrations {
  [DbContext(typeof(UsersDBContext))]
  partial class UsersDBContextModelSnapshot : ModelSnapshot {
    protected override void BuildModel(ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS")
          .HasAnnotation("Relational:MaxIdentifierLength", 128)
          .HasAnnotation("ProductVersion", "5.0.17")
          .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

      modelBuilder.Entity("DataLayer.Entityes.User", b => {
        b.Property<int>("Id")
            .ValueGeneratedOnAdd()
            .HasColumnType("int")
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        b.Property<int?>("Age")
            .ValueGeneratedOnAdd()
            .HasColumnType("int")
            .HasDefaultValueSql("((0))");

        b.Property<string>("Email")
            .HasMaxLength(30)
            .IsUnicode(false)
            .HasColumnType("varchar(30)");

        b.Property<string>("Name")
            .HasMaxLength(30)
            .HasColumnType("nvarchar(30)");

        b.Property<DateTime?>("RegistationDate")
            .ValueGeneratedOnAdd()
            .HasColumnType("date")
            .HasDefaultValueSql("(getdate())");

        b.Property<string>("Surname")
            .HasMaxLength(30)
            .HasColumnType("nvarchar(30)");

        b.HasKey("Id");

        b.HasIndex(new[] { "Email" }, "UQ__users__A9D105342BE8BF8F")
            .IsUnique()
            .HasFilter("[Email] IS NOT NULL");

        b.ToTable("users");
      });
#pragma warning restore 612, 618
    }
  }
}
