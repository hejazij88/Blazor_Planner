﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Planner_Business;

#nullable disable

namespace Planner.Business.Migrations
{
    [DbContext(typeof(PlannerContext))]
    [Migration("20240427150618_editRelation")]
    partial class editRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Planner.Domain.Model.DatePlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DatePlan");
                });

            modelBuilder.Entity("Planner_Domain.Model.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ToDoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DateId");

                    b.HasIndex("ToDoId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("Planner_Domain.Model.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Planner_Domain.Model.ToDo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ToDo");
                });

            modelBuilder.Entity("Planner_Domain.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Planner.Domain.Model.DatePlan", b =>
                {
                    b.HasOne("Planner_Domain.Model.User", "User")
                        .WithMany("DatePlans")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Planner_Domain.Model.Activity", b =>
                {
                    b.HasOne("Planner.Domain.Model.DatePlan", "DatePlan")
                        .WithMany("Activities")
                        .HasForeignKey("DateId");

                    b.HasOne("Planner_Domain.Model.ToDo", "ToDo")
                        .WithMany("Activities")
                        .HasForeignKey("ToDoId");

                    b.Navigation("DatePlan");

                    b.Navigation("ToDo");
                });

            modelBuilder.Entity("Planner_Domain.Model.Category", b =>
                {
                    b.HasOne("Planner_Domain.Model.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Planner_Domain.Model.ToDo", b =>
                {
                    b.HasOne("Planner_Domain.Model.Category", "Category")
                        .WithMany("ToDoList")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Planner.Domain.Model.DatePlan", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("Planner_Domain.Model.Category", b =>
                {
                    b.Navigation("ToDoList");
                });

            modelBuilder.Entity("Planner_Domain.Model.ToDo", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("Planner_Domain.Model.User", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("DatePlans");
                });
#pragma warning restore 612, 618
        }
    }
}
