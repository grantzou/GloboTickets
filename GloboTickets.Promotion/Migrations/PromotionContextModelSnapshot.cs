﻿// <auto-generated />
using System;
using GloboTickets.Promotion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloboTickets.Promotion.Migrations
{
    [DbContext(typeof(PromotionContext))]
    partial class PromotionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("GloboTickets.Promotion.Acts.Act", b =>
                {
                    b.Property<int>("ActId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<Guid>("ActGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ActId");

                    b.HasAlternateKey("ActGuid");

                    b.ToTable("Act");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Acts.ActDescription", b =>
                {
                    b.Property<int>("ActDescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ActId")
                        .HasColumnType("int");

                    b.Property<string>("ImageHash")
                        .HasMaxLength(88)
                        .HasColumnType("nvarchar(88)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ActDescriptionId");

                    b.HasAlternateKey("ActId", "ModifiedDate");

                    b.ToTable("ActDescription");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Acts.ActRemoved", b =>
                {
                    b.Property<int>("ActRemovedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ActId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RemovedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ActRemovedId");

                    b.HasAlternateKey("ActId", "RemovedDate");

                    b.ToTable("ActRemoved");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Contents.Content", b =>
                {
                    b.Property<string>("Hash")
                        .HasMaxLength(88)
                        .HasColumnType("nvarchar(88)");

                    b.Property<byte[]>("Binary")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Hash");

                    b.ToTable("Content");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Shows.Show", b =>
                {
                    b.Property<int>("ShowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ActId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("ShowId");

                    b.HasAlternateKey("ActId", "VenueId", "StartTime");

                    b.HasIndex("VenueId");

                    b.ToTable("Show");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Shows.ShowCancelled", b =>
                {
                    b.Property<int>("ShowCancelledId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CancelledDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.HasKey("ShowCancelledId");

                    b.HasAlternateKey("ShowId", "CancelledDate");

                    b.ToTable("ShowCancelled");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<Guid>("VenueGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VenueId");

                    b.HasAlternateKey("VenueGuid");

                    b.ToTable("Venue");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.VenueDescription", b =>
                {
                    b.Property<int>("VenueDescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("VenueDescriptionId");

                    b.HasIndex("VenueId");

                    b.ToTable("VenueDescription");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.VenueLocation", b =>
                {
                    b.Property<int>("VenueLocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("VenueLocationId");

                    b.HasIndex("VenueId");

                    b.ToTable("VenueLocation");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.VenueRemoved", b =>
                {
                    b.Property<int>("VenueRemovedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("RemovedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("VenueRemovedId");

                    b.HasIndex("VenueId");

                    b.ToTable("VenueRemoved");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.VenueTimeZone", b =>
                {
                    b.Property<int>("VenueTimeZoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("VenueTimeZoneId");

                    b.HasIndex("VenueId");

                    b.ToTable("VenueTimeZone");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Acts.ActDescription", b =>
                {
                    b.HasOne("GloboTickets.Promotion.Acts.Act", "Act")
                        .WithMany("Descriptions")
                        .HasForeignKey("ActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Act");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Acts.ActRemoved", b =>
                {
                    b.HasOne("GloboTickets.Promotion.Acts.Act", "Act")
                        .WithMany("Removed")
                        .HasForeignKey("ActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Act");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Shows.Show", b =>
                {
                    b.HasOne("GloboTickets.Promotion.Acts.Act", "Act")
                        .WithMany()
                        .HasForeignKey("ActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GloboTickets.Promotion.Venues.Venue", "Venue")
                        .WithMany()
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Act");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Shows.ShowCancelled", b =>
                {
                    b.HasOne("GloboTickets.Promotion.Shows.Show", "Show")
                        .WithMany("Cancelled")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Show");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.VenueDescription", b =>
                {
                    b.HasOne("GloboTickets.Promotion.Venues.Venue", "Venue")
                        .WithMany("Descriptions")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.VenueLocation", b =>
                {
                    b.HasOne("GloboTickets.Promotion.Venues.Venue", "Venue")
                        .WithMany("Locations")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.VenueRemoved", b =>
                {
                    b.HasOne("GloboTickets.Promotion.Venues.Venue", "Venue")
                        .WithMany("Removed")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.VenueTimeZone", b =>
                {
                    b.HasOne("GloboTickets.Promotion.Venues.Venue", "Venue")
                        .WithMany("TimeZones")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Acts.Act", b =>
                {
                    b.Navigation("Descriptions");

                    b.Navigation("Removed");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Shows.Show", b =>
                {
                    b.Navigation("Cancelled");
                });

            modelBuilder.Entity("GloboTickets.Promotion.Venues.Venue", b =>
                {
                    b.Navigation("Descriptions");

                    b.Navigation("Locations");

                    b.Navigation("Removed");

                    b.Navigation("TimeZones");
                });
#pragma warning restore 612, 618
        }
    }
}
