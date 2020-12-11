﻿// <auto-generated />
using System;
using FacebookWallDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FacebookWallDataAccessLibrary.Migrations
{
    [DbContext(typeof(WallContext))]
    [Migration("20201210215856_DateTimeNowForReply")]
    partial class DateTimeNowForReply
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("FacebookWallDataAccessLibrary.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("FacebookWallDataAccessLibrary.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("FacebookWallDataAccessLibrary.Models.Reply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PostId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("FacebookWallDataAccessLibrary.Models.Post", b =>
                {
                    b.HasOne("FacebookWallDataAccessLibrary.Models.Person", null)
                        .WithMany("Posts")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("FacebookWallDataAccessLibrary.Models.Reply", b =>
                {
                    b.HasOne("FacebookWallDataAccessLibrary.Models.Person", null)
                        .WithMany("Comments")
                        .HasForeignKey("PersonId");

                    b.HasOne("FacebookWallDataAccessLibrary.Models.Post", null)
                        .WithMany("Replies")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("FacebookWallDataAccessLibrary.Models.Person", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("FacebookWallDataAccessLibrary.Models.Post", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
