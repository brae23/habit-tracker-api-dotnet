﻿// <auto-generated />
using System;
using HabitTracker.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HabitTracker.Api.Migrations
{
    [DbContext(typeof(HabitTrackerDbContext))]
    [Migration("20240610150306_InitialDbContext")]
    partial class InitialDbContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("HabitTracker.Api.Infrastructure.Entities.List", b =>
                {
                    b.Property<Guid>("ListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ListId");

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("HabitTracker.Api.Infrastructure.Entities.Task", b =>
                {
                    b.Property<Guid>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Completed")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("CompletedByUserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasChildTasks")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ParentListId")
                        .HasColumnType("TEXT");

                    b.HasKey("TaskId");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ParentListId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("HabitTracker.Api.Infrastructure.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AccountCreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HabitTracker.Api.Infrastructure.Entities.Task", b =>
                {
                    b.HasOne("HabitTracker.Api.Infrastructure.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HabitTracker.Api.Infrastructure.Entities.List", null)
                        .WithMany("ListItems")
                        .HasForeignKey("ParentListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HabitTracker.Api.Infrastructure.Entities.List", b =>
                {
                    b.Navigation("ListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
