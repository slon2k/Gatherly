﻿// <auto-generated />
using System;
using Gatherly.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gatherly.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221225095150_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gatherly.Domain.Entities.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("Date");

                    b.Property<Guid>("GatheringId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GatheringId");

                    b.HasIndex("MemberId", "GatheringId")
                        .IsUnique();

                    b.ToTable("Attendee", (string)null);
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Gathering", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("InvitationsExpireAt")
                        .HasColumnType("Date");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("MaxNumberOfAttendees")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("Date");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Gathering", (string)null);
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("Date");

                    b.Property<Guid>("GatheringId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.HasIndex("GatheringId");

                    b.HasIndex("MemberId");

                    b.ToTable("Invitation", (string)null);
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LastName");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Member", (string)null);
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Attendee", b =>
                {
                    b.HasOne("Gatherly.Domain.Entities.Gathering", "Gathering")
                        .WithMany("Attendees")
                        .HasForeignKey("GatheringId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gatherly.Domain.Entities.Member", "Member")
                        .WithMany("Attendees")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Gathering");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Gathering", b =>
                {
                    b.HasOne("Gatherly.Domain.Entities.Member", "Creator")
                        .WithMany("GatheringsCreated")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Invitation", b =>
                {
                    b.HasOne("Gatherly.Domain.Entities.Gathering", "Gathering")
                        .WithMany("Invitations")
                        .HasForeignKey("GatheringId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gatherly.Domain.Entities.Member", "Member")
                        .WithMany("Invitations")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Gathering");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Gathering", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("Invitations");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Member", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("GatheringsCreated");

                    b.Navigation("Invitations");
                });
#pragma warning restore 612, 618
        }
    }
}
